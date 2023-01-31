using NETCore.Encrypt;

var (pub,pri) = EncryptProvider.RSAToPem(true, 2048);

Console.WriteLine(pub);
Console.WriteLine(pri);

var str = "catcherwong";

var encByte = EncryptProvider.RSAEncryptWithPem(pub, System.Text.Encoding.UTF8.GetBytes(str));
var encStr =  Convert.ToBase64String(encByte);

Console.WriteLine(encStr);

var dec = EncryptProvider.RSADecryptWithPem(pri , Convert.FromBase64String(encStr));
var decStr = System.Text.Encoding.UTF8.GetString(dec);

Console.WriteLine(decStr);