#!/bin/bash
set -e
cd /opt/pekinteknoloji

if ! docker image inspect pekin/nopcommerce:rollback &>/dev/null; then
    echo "Rollback image bulunamadı. Daha önce deploy yapılmamış olabilir."
    exit 1
fi

echo "Önceki versiyona dönülüyor..."
docker tag pekin/nopcommerce:rollback pekin/nopcommerce:latest
docker compose up -d --no-build nopcommerce_web

echo "Rollback tamamlandı."
