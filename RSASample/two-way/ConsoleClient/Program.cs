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
            var serverPub = GetPublicKey(_appId);

            if (string.IsNullOrWhiteSpace(serverPub))
            {
                Console.WriteLine("get publicKey error");
                return;
            }

            // 2. generate client rsa
            var (clientPub, clientPri) = EncryptProvider.RSAToPem(true, 2048);

            var clientPubBytes = Encoding.UTF8.GetBytes(clientPub).AsSpan();
            List<byte> enBytes = new();
            for (int i = 0; i < clientPubBytes.Length; i += 190)
            {
                var bytes = clientPubBytes.Slice(i, Math.Min(190, clientPubBytes.Length - i)).ToArray();
                enBytes.AddRange(EncryptProvider.RSAEncrypt(serverPub, bytes, RSAEncryptionPadding.Pkcs1, true));
            }

            // 3. use server rsa public key to encrypt client public key
            var encClientPub = Convert.ToBase64String(enBytes.ToArray());

            // 4. send encrypted client public key to server 
            // 5. get the encrypted aes key from server
            var encAesKey = SetClientPublicKey(_appId, encClientPub);

            // 6. use client private key to decrypt the encrypted aes key
            var aesKey = EncryptProvider.RSADecrypt(clientPri, encAesKey, RSAEncryptionPadding.Pkcs1, true);
            Console.WriteLine(aesKey);

            // 7. use aes key to encrypt request data
            var encData = EncryptProvider.AESEncrypt("catcherwong", aesKey);

            // 8. send the encrypted request data to server
            var respData = BizReq(_appId, encData);

            Console.WriteLine(respData);
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

        static string? SetClientPublicKey(string appId, string enc)
        {
            var url = $"{_url}/com/set-pub";

            using HttpClient client = new();
            var content = new StringContent(JsonConvert.SerializeObject(new { encParm = enc }));
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
