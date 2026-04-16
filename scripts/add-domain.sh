#!/bin/bash
set -e

DOMAIN=$1
EMAIL=${2:-admin@pekinteknoloji.com}
UPSTREAM=http://127.0.0.1:8086
SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
REPO_NGINX="$SCRIPT_DIR/../nginx/sites-available/$DOMAIN"
NGINX_AVAILABLE="/etc/nginx/sites-available/$DOMAIN"
NGINX_ENABLED="/etc/nginx/sites-enabled/$DOMAIN"

if [ -z "$DOMAIN" ]; then
    echo "Kullanım: $0 <domain> [email]"
    echo "Örnek:    $0 minikhediyen.com"
    exit 1
fi

# 1. Certbot için geçici HTTP config (henüz repo'da yoksa)
if [ ! -f "$NGINX_AVAILABLE" ]; then
    echo "→ [$DOMAIN] Geçici HTTP config oluşturuluyor (certbot için)..."
    cat > $NGINX_AVAILABLE << NGINX
server {
    listen 80;
    server_name $DOMAIN www.$DOMAIN;
    root /var/www/certbot;
    location /.well-known/acme-challenge/ { }
    location / { return 301 https://\$host\$request_uri; }
}
NGINX
    ln -sf $NGINX_AVAILABLE $NGINX_ENABLED
    mkdir -p /var/www/certbot
    nginx -t && nginx -s reload
fi

# 2. SSL sertifikası al
echo "→ [$DOMAIN] SSL sertifikası alınıyor..."
certbot certonly --webroot -w /var/www/certbot -d $DOMAIN --agree-tos --email $EMAIL -n || true

# 3. Config'i repo'dan kopyala (varsa), yoksa template'den oluştur
if [ -f "$REPO_NGINX" ]; then
    echo "→ [$DOMAIN] Config repo'dan kopyalanıyor..."
    cp "$REPO_NGINX" $NGINX_AVAILABLE
else
    echo "→ [$DOMAIN] HTTPS config oluşturuluyor..."
    cat > $NGINX_AVAILABLE << NGINX
server {
    listen 80;
    server_name $DOMAIN www.$DOMAIN;
    return 301 https://$DOMAIN\$request_uri;
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
    proxy_http_version 1.1;
    proxy_buffering on;
    proxy_buffer_size 256k;
    proxy_buffers 8 512k;
    proxy_busy_buffers_size 1m;
    proxy_ignore_client_abort on;
    proxy_read_timeout 300s;
    proxy_connect_timeout 300s;
    proxy_send_timeout 300s;

    location / {
        proxy_pass $UPSTREAM;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto https;
        proxy_set_header Connection "";
        proxy_hide_header Transfer-Encoding;
        proxy_hide_header Upgrade;
    }
}
NGINX
fi

ln -sf $NGINX_AVAILABLE $NGINX_ENABLED
nginx -t && nginx -s reload
echo "✓ [$DOMAIN] Tamamlandı"
