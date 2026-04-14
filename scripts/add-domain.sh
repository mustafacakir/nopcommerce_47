#!/bin/bash
set -e

DOMAIN=$1
EMAIL=${2:-admin@pekinteknoloji.com}
UPSTREAM=http://127.0.0.1:8086

if [ -z "$DOMAIN" ]; then
    echo "Kullanım: $0 <domain> [email]"
    echo "Örnek:    $0 minikhediyen.com"
    exit 1
fi

echo "→ [$DOMAIN] Nginx HTTP config oluşturuluyor..."
cat > /etc/nginx/sites-available/$DOMAIN << NGINX
server {
    listen 80;
    server_name $DOMAIN www.$DOMAIN;
    root /var/www/certbot;
    location /.well-known/acme-challenge/ { }
    location / { return 301 https://\$host\$request_uri; }
}
NGINX

ln -sf /etc/nginx/sites-available/$DOMAIN /etc/nginx/sites-enabled/$DOMAIN
mkdir -p /var/www/certbot
nginx -t && nginx -s reload

echo "→ [$DOMAIN] SSL sertifikası alınıyor..."
certbot certonly --webroot -w /var/www/certbot \
    -d $DOMAIN -d www.$DOMAIN \
    --non-interactive --agree-tos --email $EMAIL

echo "→ [$DOMAIN] HTTPS config yazılıyor..."
cat > /etc/nginx/sites-available/$DOMAIN << NGINX
server {
    listen 80;
    server_name $DOMAIN www.$DOMAIN;
    return 301 https://\$host\$request_uri;
}

server {
    listen 443 ssl;
    server_name $DOMAIN www.$DOMAIN;

    ssl_certificate /etc/letsencrypt/live/$DOMAIN/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/$DOMAIN/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    client_max_body_size 64m;
    proxy_read_timeout 300s;

    location / {
        proxy_pass $UPSTREAM;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto https;
    }
}
NGINX

nginx -t && nginx -s reload
echo "✓ [$DOMAIN] Tamamlandı"
