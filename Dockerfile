# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
# Git reposunun yapısına göre kaynak dosyaları kopyala
# Server: Presentation/ doğrudan kök dizinde
# Local dev: publish_folder kullanılır (aşağıdaki COPY --from=build ile override edilir)
COPY . .
RUN if [ -f "Presentation/Nop.Web/Nop.Web.csproj" ]; then \
      dotnet publish Presentation/Nop.Web/Nop.Web.csproj -c Release -o /app/publish; \
    else \
      dotnet publish src/Presentation/Nop.Web/Nop.Web.csproj -c Release -o /app/publish; \
    fi

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# add globalization support
RUN apk add --no-cache icu-libs icu-data-full
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# installs required packages
RUN apk add tiff --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/main/ --allow-untrusted
RUN apk add libgdiplus --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/community/ --allow-untrusted
RUN apk add libc-dev tzdata --no-cache

# copy entrypoint script
COPY ./entrypoint.sh /entrypoint.sh
RUN chmod 755 /entrypoint.sh

WORKDIR /app

COPY --from=build /app/publish .

RUN echo '{"DataProvider":"postgresql","ConnectionString":"Host=pekin_pgbouncer;Database=nopcommerce_db;Username=nopuser;Password=NopPass2024","RawDataSettings":{}}' \
    > /app/App_Data/dataSettings.json

RUN mkdir -p logs bin App_Data App_Data/DataProtectionKeys Plugins \
              wwwroot/bundles wwwroot/db_backups wwwroot/files/exportimport \
              wwwroot/icons wwwroot/images wwwroot/images/thumbs \
              wwwroot/images/uploaded wwwroot/sitemaps && \
    chmod 775 App_Data \
              App_Data/DataProtectionKeys \
              bin \
              logs \
              Plugins \
              wwwroot/bundles \
              wwwroot/db_backups \
              wwwroot/files/exportimport \
              wwwroot/icons \
              wwwroot/images \
              wwwroot/images/thumbs \
              wwwroot/images/uploaded \
              wwwroot/sitemaps

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT "/entrypoint.sh"
