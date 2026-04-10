server {
    listen 80;
    server_name *.pekinteknoloji.com;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl;
    server_name *.pekinteknoloji.com;

    ssl_certificate /etc/letsencrypt/live/pekinteknoloji.com-0001/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/pekinteknoloji.com-0001/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    location / {
        proxy_pass http://127.0.0.1:5102;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_read_timeout 300s;
        client_max_body_size 64m;
    }
}
