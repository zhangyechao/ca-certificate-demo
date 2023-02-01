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
        static string _url = "http://localhost:7777";

        static void Main()
        {
            // 1. get rsa public key from server
            var pub = GetPublicKey(_appId);

            if (string.IsNullOrWhiteSpace(pub))
            {
                Console.WriteLine("get publicKey error");
                return;
            }

            // 2. generate aes key
            var aesKey = GenAesKey();
            Console.WriteLine(aesKey);

            // 3. use rsa public key to encrypt the aes key
            var encAesKey = EncryptProvider.RSAEncrypt(pub, aesKey, RSAEncryptionPadding.Pkcs1, true);
            
            // 4. send the encryptd aes key to server
            SetAesKey(_appId, encAesKey);

            // 5. use aes key to encrypt request data
            var encData = EncryptProvider.AESEncrypt("catcherwong", aesKey);

            // 6. send the encrypted request data to server
            var respData = BizReq(_appId, encData);

            Console.WriteLine(respData);
        }

        static string GenAesKey()
        {
            StringBuilder sb = new();
            for (int i = 0; i < 8; i++)
            {
                sb.Append(new Random().Next(1000, 9999));
            }

            return sb.ToString();
        }

        static string? GetPublicKey(string appId)
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

        static bool SetAesKey(string appId, string encAesKey)
        {
            var url = $"{_url}/com/set-key";

            using HttpClient client = new();
            var content = new StringContent(JsonConvert.SerializeObject(new { encParm = encAesKey }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.TryAddWithoutValidation("appId", appId);

            var resp = client.PostAsync(url, content).GetAwaiter().GetResult();
            return resp.IsSuccessStatusCode;
        }

        static string? BizReq(string appId, string data)
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
