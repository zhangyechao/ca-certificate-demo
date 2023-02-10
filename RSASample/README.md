# RSASample

## zero-way

1. [client] get rsa public key from server
2. [client] use rsa public key to encrypt the aes key
3. [client] send encrypted request data to server
4. [server] dectcrypt request data
5. [server] handle biz

## one-way

1. [client] get rsa public key from server
2. [client] generate aes key
3. [client] use rsa public key to encrypt the aes key
4. [client] use aes key to encrypt request data
5. [client] send encrypted request data to server
6. [server] dectcrypt request data
7. [server] handle biz


## two-way

1. [client] get rsa public key from server
2. [client] generate a new client rsa
3. [client] send client public key to server 
4. [server] return a encrypted aes key with client public key
5. [client] dectcrypt encrypted aes key and store it
6. [client] use aes key to encrypt request data
7. [server] send the encrypted request data to server
8. [server] dectcrypt request data
9. [server] handle biz


The following blog shows more details on this sample.

https://www.cnblogs.com/catcher1994/p/17106584.html

https://mp.weixin.qq.com/s/pAKl8p19llD20AjrGM8uHA

