# MutualTLSAuthentication

A sample to show mutual tls authentucatuib.

```
├─ConsoleApp
├─CusSSLLib
├─withiis
│  └─WebApi
├─withnginx
│  ├─nginx
│  ├─WebApi
│  └─docker-compose.yml
└─without3rdserver
    └─WebApi
```

1. asp.net core with certificate
2. asp.net core without certificate + nginx with certificate
3. asp.net core without certificate + iis with certificate


```sh
# CA
openssl genrsa -out ca.key 4096
openssl req -new -key ca.key -out ca.csr -days 365
openssl x509 -req -in ca.csr -signkey ca.key -out ca.crt -days 365

# Server
openssl genrsa -out server.key 4096
openssl req -new -key server.key -out server.csr -days 365
openssl x509 -req -in server.csr -out server.crt -CA ca.crt  -CAkey ca.key  -CAcreateserial -days 365
openssl pkcs12 -export -in server.crt -inkey server.key -out server.p12

# Client
openssl genrsa -out client.key 4096
openssl req -new -key client.key -out client.csr -days 365
openssl x509 -req -in client.csr -out client.crt -CA ca.crt  -CAkey ca.key  -CAcreateserial -days 365
openssl pkcs12 -export -in client.crt -inkey client.key -out client.p12
```
# OpenSSL 来生成一个自签名的证书 2 个根证书，1 个服务端证书，2个不是同一个根证书下面的客户端证书
```sh
# 根证书
openssl genrsa -out ca.key 4096
openssl req -new -key ca.key -out ca.csr -days 365
openssl x509 -req -in ca.csr -signkey ca.key -out ca.crt -days 365

# 服务端证书
openssl genrsa -out server.key 4096
openssl req -new -key server.key -out server.csr -days 365
openssl x509 -req -in server.csr -out server.crt -CA ca.crt  -CAkey ca.key  -CAcreateserial -days 365
openssl pkcs12 -export -in server.crt -inkey server.key -out server.p12

# 客户端证书
openssl genrsa -out client.key 4096
openssl req -new -key client.key -out client.csr -days 365
openssl x509 -req -in client.csr -out client.crt -CA ca.crt  -CAkey ca.key  -CAcreateserial -days 365
openssl pkcs12 -export -in client.crt -inkey client.key -out client.p12
```
```sh
#nginx 反向代理

server {
        listen       443 ssl;
        server_name  localhost;

        # server certificate
        ssl_certificate  /etc/nginx/ssl/server.crt;
        ssl_certificate_key /etc/nginx/ssl/server.key;

        # root certificate
        ssl_client_certificate /etc/nginx/ssl/ca.crt;
        # open client certificate verify
        ssl_verify_client on;
        ssl_session_timeout  5m;            

        location / {
            proxy_pass http://webapi;
            index  index.html;
        }
    }
