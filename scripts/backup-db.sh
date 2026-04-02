#!/bin/bash

BACKUP_DIR="/opt/pekinteknoloji/backups"
DATE=$(date +%Y%m%d_%H%M%S)
BACKUP_FILE="$BACKUP_DIR/nopcommerce_$DATE.sql.gz"
RETAIN_DAYS=7

mkdir -p "$BACKUP_DIR"

docker exec pekin_db pg_dump -U nopuser nopcommerce_db | gzip > "$BACKUP_FILE"

if [ $? -eq 0 ]; then
    echo "[$DATE] Backup OK: $BACKUP_FILE ($(du -sh "$BACKUP_FILE" | cut -f1))"
else
    echo "[$DATE] Backup FAILED!" >&2
    rm -f "$BACKUP_FILE"
    exit 1
fi

# 7 günden eski backup'ları sil
find "$BACKUP_DIR" -name "nopcommerce_*.sql.gz" -mtime +$RETAIN_DAYS -delete
echo "[$DATE] Eski backup'lar temizlendi (>${RETAIN_DAYS} gün)"
