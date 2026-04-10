#!/bin/bash
set -e
cd /opt/pekinteknoloji
chmod +x deploy.sh rollback.sh

SKIP_ROLLBACK=0
if [ "$1" = "-n" ]; then
  SKIP_ROLLBACK=1
fi

echo "[1/5] Git pull..."
git pull

if [ "$SKIP_ROLLBACK" = "0" ]; then
  echo "[2/5] DB yedeği alınıyor..."
  BACKUP_DIR="/opt/pekinteknoloji/backups"
  mkdir -p "$BACKUP_DIR"
  BACKUP_FILE="$BACKUP_DIR/db_$(date +%Y%m%d_%H%M%S).sql.gz"
  docker exec nop_db pg_dump -U nopuser nopcommerce_db | gzip > "$BACKUP_FILE"
  echo "  Yedek: $BACKUP_FILE"
  find "$BACKUP_DIR" -name "db_*.sql.gz" -mtime +7 -delete
else
  echo "[2/5] DB yedeği atlandı (-n flag)."
fi

if [ "$SKIP_ROLLBACK" = "0" ]; then
  echo "[3/5] Mevcut image rollback olarak etiketleniyor..."
  docker tag pekin/nopcommerce:latest pekin/nopcommerce:rollback 2>/dev/null || echo "  (ilk deploy, rollback yok)"
else
  echo "[3/5] Rollback atlandı (-n flag)."
fi

echo "[4/5] Yeni image build ediliyor..."
docker compose build nopcommerce_web

echo "[5/5] Container yeniden başlatılıyor..."
docker compose up -d nopcommerce_web

echo ""
echo "Deploy tamamlandı."
[ "$SKIP_ROLLBACK" = "0" ] && echo "Sorun olursa: bash rollback.sh"
