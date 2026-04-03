#!/bin/bash
# Yeni custom domain'ler için otomatik SSL sertifikası alır ve nginx config oluşturur.
# Cron ile saatte bir çalıştırılmalı:
#   0 * * * * /opt/pekinteknoloji/scripts/auto-ssl.sh >> /var/log/auto-ssl.log 2>&1

set -e

EMAIL="admin@pekinteknoloji.com"
NGINX_CONF_DIR="/etc/nginx/sites-enabled"
PROXY_PASS="http://127.0.0.1:8086"

echo "[$(date)] SSL kontrolü başlıyor..."

# DB'den pekinteknoloji.com dışındaki custom domain'leri çek
DOMAINS=$(docker exec pekin_db psql -U nopuser nopcommerce_db -t -A -c \
  "SELECT REGEXP_REPLACE(LOWER(url), '^https?://', '') FROM Store WHERE url NOT LIKE '%pekinteknoloji.com%' AND url IS NOT NULL AND url != '';" \
  | tr -d ' ' | sed 's|/$||' | grep -v '^$' || true)

if [ -z "$DOMAINS" ]; then
  echo "[$(date)] Yeni custom domain bulunamadı."
  exit 0
fi

CHANGED=false

for DOMAIN in $DOMAINS; do
  CONF_FILE="$NGINX_CONF_DIR/$DOMAIN.conf"

  # Sertifika yoksa certbot ile al
  if [ ! -f "/etc/letsencrypt/live/$DOMAIN/fullchain.pem" ]; then
    echo "[$(date)] $DOMAIN için SSL sertifikası alınıyor..."
    certbot certonly --nginx -d "$DOMAIN" \
      --non-interactive --agree-tos -m "$EMAIL" || {
      echo "[$(date)] UYARI: $DOMAIN için sertifika alınamadı. DNS henüz yönlendirilmemiş olabilir."
      continue
    }
  fi

  # Nginx config yoksa oluştur
  if [ ! -f "$CONF_FILE" ]; then
    echo "[$(date)] $DOMAIN için nginx config oluşturuluyor..."
    cat > "$CONF_FILE" << EOF
server {
    listen 80;
    server_name $DOMAIN;
    return 301 https://\$host\$request_uri;
}

server {
    listen 443 ssl;
    server_name $DOMAIN;

    ssl_certificate /etc/letsencrypt/live/$DOMAIN/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/$DOMAIN/privkey.pem;

    client_max_body_size 64m;
    proxy_read_timeout 300;
    proxy_connect_timeout 300;
    proxy_send_timeout 300;

    location / {
        proxy_pass $PROXY_PASS;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
    }
}
EOF
    CHANGED=true
    echo "[$(date)] $DOMAIN hazır."
  fi
done

# Değişiklik varsa nginx'i yeniden yükle
if [ "$CHANGED" = true ]; then
  nginx -t && systemctl reload nginx
  echo "[$(date)] Nginx yeniden yüklendi."
fi

echo "[$(date)] SSL kontrolü tamamlandı."