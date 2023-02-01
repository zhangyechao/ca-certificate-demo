namespace WebApi
{
    public class RSAKeyMapping
    {
        public RSAKeyMapping(string appId, string pub, string pri)
        {
            this.AppId = appId;
            this.PublicKey = pub;
            this.PrivateKey = pri;
        }

        public string AppId { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public static RSAKeyMapping? GetByAppId(string appId)
        {
            var rsaKey = GetAll().FirstOrDefault(x => x.AppId == appId);
            return rsaKey;
        }

        public static string? GetPublicKeyByAppId(string appId)
        {
            var rsaKey = GetAll().FirstOrDefault(x => x.AppId == appId);
            return rsaKey?.PublicKey;
        }

        private static List<RSAKeyMapping> GetAll()
        {
            return new List<RSAKeyMapping>()
            {
                 new RSAKeyMapping("appId-1",@"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4Ll5gUn2vpwfFXAnCeUM
iQ1lP0cwWSViUh+E5PWMHsFVjhWA5Z3izZh+kND8ydqjDThA901iKXBKQqJH4o1g
y/7Dz6NAg2LCZioX1ogRx7rIdbdtJLOOQigVuyt/iHf9GWbV2GBAWVJvGnM4Q87I
okjeM/Kk1bbwHMp5Aj6YOjnMyv7YOM2wncjpaYinAbLpgebIgoX2V6Il0dhdH/Cc
8xrjMk9GmQjeh8veyNd7yNaikcsv+zzU/8Y2TMRItRxF2kEbfP6nXVOodyiSfR6w
SlBqpPnsPSaWlIwO9XnJtY2qDTEzoa7yRTrYMSfl+L0nFs/V3JgH2iKnB+xKSjrB
VQIDAQAB
-----END PUBLIC KEY-----",@"-----BEGIN PRIVATE KEY-----
MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDguXmBSfa+nB8V
cCcJ5QyJDWU/RzBZJWJSH4Tk9YwewVWOFYDlneLNmH6Q0PzJ2qMNOED3TWIpcEpC
okfijWDL/sPPo0CDYsJmKhfWiBHHush1t20ks45CKBW7K3+Id/0ZZtXYYEBZUm8a
czhDzsiiSN4z8qTVtvAcynkCPpg6OczK/tg4zbCdyOlpiKcBsumB5siChfZXoiXR
2F0f8JzzGuMyT0aZCN6Hy97I13vI1qKRyy/7PNT/xjZMxEi1HEXaQRt8/qddU6h3
KJJ9HrBKUGqk+ew9JpaUjA71ecm1jaoNMTOhrvJFOtgxJ+X4vScWz9XcmAfaIqcH
7EpKOsFVAgMBAAECggEAcQTW1u8b6UEbYOzGtXM8mrDh+rES38KtcB/p9jyH+++/
13V5HgIJztyiyrJQcDRFTKC+C6IffDe9IUX8YTQ5Ol8mm4a/K1S/8CG7W2mTh5+o
sYtOmOa05RDD3R5DRZ8S63OMmQXPVxzeQ68u3OtifDuphPbDb7hRiilKMpbIeH8n
8Q/EZB9bOW2h8Qpr2+n3vr3xyc78q1yHakbWn5IDoBoJPvhimoioF+COlWwlM5yQ
6qK66eecq4AiQjIFRJRBW/Djwx1VSFGcwUpyYcOxTeDgZMXVxxsv5CImdKzwBfkl
QXc3dbSUVYl+f7xMxvbobiqRIgHsrf34JnfmdzshAQKBgQDrCMEy3pSSP3J0TquB
0DYzgh0gdafalPJfqJmegJZdqGsYOzoJyKLC3RSNXIOKkh0CzXDR/HjaTwtRQtjn
duPfIy/cN12+2GEfgOIzYEFX7dAdqFSd9ox2kBOQv/fpCJgvk0vyQpAEXtoFIONY
rmG7Wo7bwhFyd8brA02bt2jOSwKBgQD0xUmobdQ3B8aWK+rW2lPMDUoGb8gvsBlC
YU4lt+QqBlr3gkE3sjzUVKccz0oK0M72s4xIBnNmaOz+X0LPLCB+srNMyVkjYVoj
aMwvDImvtlUxGysQiVQbDUcy+LZIqyXbczZV5nvOgTzg6QnMZSvUVTWMpY/qA5Sa
biBSPwxq3wKBgQDdSbjdNEOeDq6sYwWHi4n8pRzXWu9XFbW96vf4DAmG1PNANUNH
wue7oHuhPOF8rmUyJkt66cK/iHyXXeDqzT1u8ao2JMrWzNNk3ewbgx0CtJ2lzr8c
mu1VFAHX32aCudzPrldrGNCPzN5oxbZH4KeTxoeK0QWsSKm4y7teSSkcUQKBgAoO
1VEDelXsjt963IKJpCndXe5Xx40WGmOc5f4syUZkznYfNxjXzSIYfGl1pyA8plSC
2j/XbASISTzGo1MqCAfMxgtgtrX6eR69Xq6MQFglEOkcqa9bRUZ7TDKu/6aKydpz
qIbtGqdIrjy7trhca+mkureV0g0WqpOR9OgMSTFXAoGBAOm6C6/yXU1Vl3LGuxCp
w071sMIK8ILzfDD6B2IrNllRqkmX3foTRJKk8r/+dDbFdTd0Ya+PbkBNCSoKftxw
Kv+JOPMCLmA7ZO0d0+RxlapnyFh4ilc/iAoJFbKJH69INcNK6U4B1j+gI6urI+rm
kJc3xuGUODiSWwZ0qX/iEjVV
-----END PRIVATE KEY-----"),
                 new RSAKeyMapping("appId-2",@"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp9XLfSJud1IILeu4lI/p
goXxd8WpHNwIrUP1RH1tf7qIU8o8gEkSZzodAXBHc8kJ56H5pnnV3JPImPcCU/1/
h0Ji7YTnlYkTRMlKBpejHTSChIVKPCoMbpDN/nVu64fQMS+uKDeI6kXFAQyewRHi
hBzs7v0ZHl6XBL8Ja7r22oOHq0MOH5eFv4iZkqhOYx60VUCfg/FH6JgPDQEu89KQ
pHX+c2VflBiove5wIDBz7VhlqI0KJDORSTcyH64nEAkNqmSFhKUE4kkXj0/rOFO1
ZK6A4MpH9xOgCYp5n6hp/mfVjxentKVaVcBxFzrBt/CR9v6JBHo7MGB9y7F0vsPB
AQIDAQAB
-----END PUBLIC KEY-----",@"-----BEGIN PRIVATE KEY-----
MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCn1ct9Im53Uggt
67iUj+mChfF3xakc3AitQ/VEfW1/uohTyjyASRJnOh0BcEdzyQnnofmmedXck8iY
9wJT/X+HQmLthOeViRNEyUoGl6MdNIKEhUo8KgxukM3+dW7rh9AxL64oN4jqRcUB
DJ7BEeKEHOzu/RkeXpcEvwlruvbag4erQw4fl4W/iJmSqE5jHrRVQJ+D8UfomA8N
AS7z0pCkdf5zZV+UGKi97nAgMHPtWGWojQokM5FJNzIfricQCQ2qZIWEpQTiSReP
T+s4U7VkroDgykf3E6AJinmfqGn+Z9WPF6e0pVpVwHEXOsG38JH2/okEejswYH3L
sXS+w8EBAgMBAAECggEABqk0h0Xbjn7B9liZncPMZ7K7L563pH6TFZVR4aL8uooD
qReoqQq9YT1sM0dqfoznCLtj9hh1xWhUGMYM4Jp5+jtHQ9f3fPbQvfUjK/nOrnUM
DQhhNtwQXD8A/e1ZNp3QRHN4/P2fQHT+aZk/n7f+/nFLcfy3h34HjleqnwzviSKI
w+XdCO0o8WJBw5z/wJj4CNY8VRhpIKBpG7ltEs2yXcSgOo5coSHCuebeTl8PsuDq
0uursh18QYUWAfH5ImSI2UD67UPdBNPIoWGH3POxlFCA6bldXhGhbOKFTHEZXs0a
lMKqy53b7dytgSKz/Ip+yuF8pikFmWLbrvFJWsQ7pQKBgQDXMJoePqteHWX6f06/
ke9HNwrpKpxwdE+ch/QFZUiGkU+o4BcsXFt1XXZsEXJIEpvnOzBha9OufE6avWLX
Atdbmb45ZlyqxdKhX1DrinW8Mka9T0PlkAooA8FbZRv+Ddnd8Y2gp62wWtmtgSsH
8Fnc8Xqn7T4GE4qEEGVIXN3orwKBgQDHqiL9L72c/9CesdXUvNbIYDm7I54MAkFj
Dc7IGEGuZYf4jA/6YPCwtBIHSC+lnEZqKvnH8Qx8C6tR5M5OigcnHJNh2IVYw4HV
cgXEFdibM5QIPuuOD1d4XZW5DYeeDBEHq/um9hGYGCwEqaCX5dsUKQTDGzRbdSzg
dNRDmuz9TwKBgQCSWSiHp0FuyXmgilJpMPvVqBaQiv1H8aJvJ3sK7F4Nvue5R2Yb
Mli1EjJGvvrFD3reIL//kZCuWzYuQNzms7T5RC895GLjdZSRUTy8ZDc8HclEWJMp
nfN6UUuAN9x4xLzGF9ClUURr6u2d9XnpdUn4XHZ5nHyRL1sGBAyN0TLOWwKBgQCe
pfeLSiXV3AZ+Mjv//9SB/sOgiRqJ3DjsZgpKP8vgsNgri88eWWymW05/7sG9S8E/
lspGLqiG4BbOyYmMKmyojTImaw75is9dNG6Uce9c0szrCGPOyy3rfCD9m1yJHnlh
qSwabCdqvLotMkNirsc266jXBoTBrwdriiknVrC8TQKBgQCQJt+BOU2jge04/PpO
3M7n4Dx/cT5lnEn28vW2+zI8YcrJ8hsx/+OVOvNZz2Vt+Vt9bQlpIwfC6KhGNb6j
NOvOP0Tz1lPlWZu22S+ClIgqcC3D4Tmb+hM8TPAeTSUM5mCsRrG/mDt4AupqV+eJ
Dx9Mfv0j/muRtaeBxXx1Vsgi8A==
-----END PRIVATE KEY-----"),
            };
        }
    }
}
