server {
    listen 80;
    server_name *.pekinteknoloji.com;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl;
    http2 off;
    server_name *.pekinteknoloji.com;

    ssl_certificate /etc/letsencrypt/live/pekinteknoloji.com-0001/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/pekinteknoloji.com-0001/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    gzip on;
    gzip_vary on;
    gzip_proxied any;
    gzip_comp_level 6;
    gzip_min_length 256;
    gzip_types text/plain text/css text/xml text/javascript application/javascript application/x-javascript application/json application/xml image/svg+xml;

    client_max_body_size 64m;
    proxy_http_version 1.1;
    proxy_buffering on;
    proxy_buffer_size 256k;
    proxy_buffers 8 512k;
    proxy_busy_buffers_size 1m;
    proxy_ignore_client_abort on;
    proxy_read_timeout 300s;
    proxy_connect_timeout 300s;
    proxy_send_timeout 300s;

    location / {
        proxy_pass http://127.0.0.1:5102;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_set_header Connection "";
        proxy_hide_header Transfer-Encoding;
        proxy_hide_header Upgrade;
    }
}
