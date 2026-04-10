#!/bin/bash
set -e
cd /opt/pekinteknoloji
chmod +x deploy.sh rollback.sh

SKIP_ROLLBACK=0
if [ "$1" = "-n" ]; then
  SKIP_ROLLBACK=1
fi

echo "[1/4] Git pull..."
git pull

if [ "$SKIP_ROLLBACK" = "0" ]; then
  echo "[2/4] Mevcut image rollback olarak etiketleniyor..."
  docker tag pekin/nopcommerce:latest pekin/nopcommerce:rollback 2>/dev/null || echo "  (ilk deploy, rollback yok)"
else
  echo "[2/4] Rollback atlandı (-n flag)."
fi

echo "[3/4] Yeni image build ediliyor..."
docker compose build nopcommerce_web

echo "[4/4] Container yeniden başlatılıyor..."
docker compose up -d nopcommerce_web

echo ""
echo "Deploy tamamlandı."
[ "$SKIP_ROLLBACK" = "0" ] && echo "Sorun olursa: bash rollback.sh"
