server {
    listen 80;
    server_name minikhediyen.com www.minikhediyen.com;
    return 301 https://minikhediyen.com$request_uri;
}

server {
    listen 443 ssl;
    server_name minikhediyen.com www.minikhediyen.com;

    ssl_certificate /etc/letsencrypt/live/minikhediyen.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/minikhediyen.com/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    client_max_body_size 64m;
    proxy_read_timeout 300s;
    proxy_http_version 1.1;
    proxy_buffering on;
    proxy_buffer_size 128k;
    proxy_buffers 16 512k;
    proxy_busy_buffers_size 1m;
    proxy_ignore_client_abort on;
    proxy_connect_timeout 300s;
    proxy_send_timeout 300s;

    location / {
        proxy_pass http://127.0.0.1:8086;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto https;
    }
}
