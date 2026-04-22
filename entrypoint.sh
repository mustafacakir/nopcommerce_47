#!/bin/sh
ln -s /lib/libc.musl-x86_64.so.1 /lib/ld-linux-x86-64.so.2 2>/dev/null || true

# Always restore config from image to prevent volume corruption issues
# Config files are tracked in git and baked into image at build time
cp /app_config/appsettings.json /app/App_Data/appsettings.json
cp /app_config/appsettings.Production.json /app/App_Data/appsettings.Production.json

# Patch plugin.json: LimitedToStores ve DisplayOrder
patch_plugin() {
    local f="$1" limited="$2" order="$3"
    [ -f "$f" ] || return
    tmp=$(mktemp)
    jq --argjson l "$limited" --argjson o "$order" \
        '.LimitedToStores = $l | .DisplayOrder = $o' "$f" > "$tmp" && mv "$tmp" "$f"
}

patch_plugin /app/Plugins/Widgets.HeroBanner/plugin.json          '[31]' 1
patch_plugin /app/Plugins/Widgets.TrustBadges/plugin.json         '[31]' 2
patch_plugin /app/Plugins/Widgets.ImpactCounter/plugin.json       '[31]' 3
patch_plugin /app/Plugins/Widgets.DonationCatalog/plugin.json     '[31]' 4
patch_plugin /app/Plugins/Widgets.ProjectStories/plugin.json      '[31]' 5
patch_plugin /app/Plugins/Widgets.DonationTransparency/plugin.json '[31]' 6
patch_plugin /app/Plugins/Widgets.CampaignProgress/plugin.json    '[31]' 7
patch_plugin /app/Plugins/Widgets.RecentDonations/plugin.json     '[31]' 8
patch_plugin /app/Plugins/Widgets.RecurringDonation/plugin.json   '[31]' 9
patch_plugin /app/Plugins/Widgets.DonorStories/plugin.json        '[31]' 10
patch_plugin /app/Plugins/Widgets.VolunteerCta/plugin.json        '[31]' 11
patch_plugin /app/Plugins/Widgets.MarqueeBanner/plugin.json       '[31]' 12
patch_plugin /app/Plugins/Widgets.Whatsapp/plugin.json            '[31]' 50
patch_plugin /app/Plugins/Widgets.SocialShare/plugin.json         '[31]' 51

exec dotnet Nop.Web.dll
