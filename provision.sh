#!/bin/bash
# provision.sh — Pekin Teknoloji custom domain provisioner
# Kullanım: ./provision.sh
# Bu script /provision/pending/ dizinindeki nginx config'leri işler,
# SSL sertifikası alır ve nginx'i reload eder.
#
# Çalıştırma yeri: /opt/pekinteknoloji/
# Cron örneği: */5 * * * * /opt/pekinteknoloji/provision.sh >> /var/log/pekin-provision.log 2>&1

set -e

PROVISION_DIR="/opt/pekinteknoloji/provision"
PENDING_DIR="$PROVISION_DIR/pending"
ACTIVE_DIR="$PROVISION_DIR/active"
NGINX_CONF_DIR="/opt/pekinteknoloji/nginx/conf.d"   # pekin_web container'ının mount ettiği dizin
NGINX_CONTAINER="pekin_web"
EMAIL="bilgi@pekinteknoloji.com"

mkdir -p "$PENDING_DIR" "$ACTIVE_DIR" "$NGINX_CONF_DIR"

echo "[$(date '+%Y-%m-%d %H:%M:%S')] provision.sh başladı"

shopt -s nullglob
configs=("$PENDING_DIR"/*.conf)

if [ ${#configs[@]} -eq 0 ]; then
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] Bekleyen domain yok."
    exit 0
fi

for conf_file in "${configs[@]}"; do
    filename=$(basename "$conf_file")
    domain="${filename%.conf}"

    echo "[$(date '+%Y-%m-%d %H:%M:%S')] İşleniyor: $domain"

    # 1. HTTP nginx config'i kopyala (SSL olmadan)
    cp "$conf_file" "$NGINX_CONF_DIR/$filename"

    # 2. Nginx reload (HTTP challenge için)
    docker exec "$NGINX_CONTAINER" nginx -s reload
    echo "  Nginx reload edildi (HTTP)"

    # 3. Certbot ile SSL sertifikası al
    if certbot certonly --webroot \
        -w /var/www/certbot \
        -d "$domain" \
        -d "www.$domain" \
        --email "$EMAIL" \
        --agree-tos \
        --non-interactive \
        --quiet 2>/dev/null; then

        echo "  SSL sertifikası alındı: $domain"

        # 4. HTTPS nginx config yaz (sertifika ile)
        cat > "$NGINX_CONF_DIR/$filename" << NGINXEOF
# Auto-generated — $(date '+%Y-%m-%d %H:%M')
server {
    listen 80;
    server_name $domain www.$domain;
    return 301 https://\$host\$request_uri;
}

server {
    listen 443 ssl;
    server_name $domain www.$domain;

    ssl_certificate     /etc/letsencrypt/live/$domain/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/$domain/privkey.pem;

    ssl_protocols       TLSv1.2 TLSv1.3;
    ssl_ciphers         HIGH:!aNULL:!MD5;

    location / {
        proxy_pass         http://pekin_nopcommerce:80;
        proxy_set_header   Host              \$host;
        proxy_set_header   X-Real-IP         \$remote_addr;
        proxy_set_header   X-Forwarded-For   \$proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto \$scheme;
        proxy_read_timeout 300s;
        client_max_body_size 64m;
    }
}
NGINXEOF

    else
        echo "  UYARI: Certbot başarısız — $domain (DNS henüz yönlenmemiş olabilir, HTTP ile devam ediliyor)"
    fi

    # 5. Final nginx reload
    docker exec "$NGINX_CONTAINER" nginx -t && docker exec "$NGINX_CONTAINER" nginx -s reload
    echo "  Nginx reload edildi (final)"

    # 6. Pending'den active'e taşı
    mv "$conf_file" "$ACTIVE_DIR/$filename"
    echo "  Tamamlandı: $domain"
done

echo "[$(date '+%Y-%m-%d %H:%M:%S')] provision.sh bitti"
