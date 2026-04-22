#!/bin/sh
ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2 2>/dev/null || true

# Always restore config from image to prevent volume corruption issues
# Config files are tracked in git and baked into image at build time
cp /app_config/appsettings.json /app/App_Data/appsettings.json
cp /app_config/appsettings.Production.json /app/App_Data/appsettings.Production.json

# Patch LimitedToStores for all custom widgets to store 31
for f in /app/Plugins/Widgets.*/plugin.json; do
    tmp=$(mktemp)
    jq '.LimitedToStores = [31]' "$f" > "$tmp" && mv "$tmp" "$f"
done

exec dotnet Nop.Web.dll
