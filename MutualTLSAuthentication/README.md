# MutualTLSAuthentication

A sample to show mutual tls authentucatuib.

```
├─ConsoleApp
├─CusSSLLib
├─withnginx
│  ├─nginx
│  ├─WebApi
│  └─docker-compose.yml
└─without3rdserver
    └─WebApi
```

1. asp.net core with certificate
2. asp.net core without certificate + nginx with certificate


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
