#!/bin/bash
# Kullanım: bash db-restore.sh backups/db_20260410_120000.sql.gz
set -e

BACKUP_FILE="$1"
if [ -z "$BACKUP_FILE" ]; then
  echo "Kullanım: bash db-restore.sh <backup_dosyası>"
  echo ""
  echo "Mevcut yedekler:"
  ls -lh /opt/pekinteknoloji/backups/db_*.sql.gz 2>/dev/null || echo "  Yedek bulunamadı."
  exit 1
fi

echo "UYARI: Bu işlem mevcut DB'yi silip yedeği geri yükler!"
read -p "Devam etmek istiyor musunuz? (evet/hayır): " CONFIRM
if [ "$CONFIRM" != "evet" ]; then
  echo "İptal edildi."
  exit 0
fi

echo "DB geri yükleniyor: $BACKUP_FILE"
gunzip -c "$BACKUP_FILE" | docker exec -i nop_db psql -U nopuser nopcommerce_db
echo "DB restore tamamlandı."
