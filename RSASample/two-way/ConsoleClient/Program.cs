using NETCore.Encrypt;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleClient
{
    public class Program
    {
        static string _appId = "appId-1";
        static string _url = "http://localhost:7779";

        static void Main()
        {
            // 1. get rsa public key from server
            var serverPub = GetPublicKeyFromServer(_appId);

            if (string.IsNullOrWhiteSpace(serverPub))
            {
                Console.WriteLine("get publicKey error");
                return;
            }

            // 2. generate a new client rsa, or save rsa in configuration file
            var (clientPub, clientPri) = EncryptProvider.RSAToPem(true, 2048);

            // 3. send client public key to server and get the encrypted aes key from server
            var encAesKey = SendCPK2ServerAndGetAKFromServer(_appId, clientPub);
            
            if (string.IsNullOrWhiteSpace(encAesKey))
            {
                Console.WriteLine("get aesKey error");
                return;
            }

            Console.WriteLine($"encAesKey={encAesKey}");

            // 4. use client private key to decrypt the encrypted aes key
            var aesKeyBytes = EncryptProvider.RSADecrypt(clientPri, Convert.FromBase64String(encAesKey), RSAEncryptionPadding.Pkcs1, true);
            var aesKey = Encoding.UTF8.GetString(aesKeyBytes);
            Console.WriteLine($"aesKey={aesKey}");

            // 5. use aes key to encrypt request data
            var encData = EncryptProvider.AESEncrypt("catcherwong", aesKey);

            // 6. send the encrypted request data to server
            var respData = SendBizReq(_appId, encData);

            Console.WriteLine(respData);
        }

        static string? GetPublicKeyFromServer(string appId)
        {
            var url = $"{_url}/com/req-pub?appId={appId}";

            using HttpClient client = new();
            var resp = client.GetAsync(url).GetAwaiter().GetResult();
            if (resp.IsSuccessStatusCode)
            {
                var key = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Newtonsoft.Json.Linq.JObject.Parse(key).TryGetValue("data", out var value);
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        static string? SendCPK2ServerAndGetAKFromServer(string appId, string enc)
        {
            var url = $"{_url}/com/set-pub";

            using HttpClient client = new();
            var content = new StringContent(JsonConvert.SerializeObject(enc));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.TryAddWithoutValidation("appId", appId);

            var resp = client.PostAsync(url, content).GetAwaiter().GetResult();
            if (resp.IsSuccessStatusCode)
            {
                var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Newtonsoft.Json.Linq.JObject.Parse(str).TryGetValue("data", out var value);
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        static string? SendBizReq(string appId, string data)
        {
            var url = $"{_url}/biz";

            using HttpClient client = new();
            var content = new StringContent(JsonConvert.SerializeObject(new { encParm = data }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.TryAddWithoutValidation("appId", appId);

            var resp = client.PostAsync(url, content).GetAwaiter().GetResult();
            if (resp.IsSuccessStatusCode)
            {
                var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return str;
            }
            else
            {
                return null;
            }
        }
    }
}
