using NETCore.Encrypt;

var (pub,pri) = EncryptProvider.RSAToPem(true, 2048);

Console.WriteLine(pub);
Console.WriteLine(pri);

var str = "catcherwong";

var encByte = EncryptProvider.RSAEncrypt(pub, System.Text.Encoding.UTF8.GetBytes(str), System.Security.Cryptography.RSAEncryptionPadding.Pkcs1, true);
var encStr =  Convert.ToBase64String(encByte);

Console.WriteLine(encStr);

var dec = EncryptProvider.RSADecrypt(pri , Convert.FromBase64String(encStr), System.Security.Cryptography.RSAEncryptionPadding.Pkcs1, true);
var decStr = System.Text.Encoding.UTF8.GetString(dec);

Console.WriteLine(decStr);