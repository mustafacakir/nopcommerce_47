#!/bin/bash
set -e
cd /opt/pekinteknoloji
chmod +x deploy.sh rollback.sh

echo "[1/4] Git pull..."
git pull

echo "[2/4] Mevcut image rollback olarak etiketleniyor..."
docker tag pekin/nopcommerce:latest pekin/nopcommerce:rollback 2>/dev/null || echo "  (ilk deploy, rollback yok)"

echo "[3/4] Yeni image build ediliyor..."
docker compose build nopcommerce_web

echo "[4/4] Container yeniden başlatılıyor..."
docker compose up -d nopcommerce_web

echo ""
echo "Deploy tamamlandı."
echo "Sorun olursa: ./rollback.sh"
