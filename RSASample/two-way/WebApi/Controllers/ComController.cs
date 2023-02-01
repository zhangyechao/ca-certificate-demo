using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using System.Security.Cryptography;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComController : ControllerBase
    {
        [HttpGet("req-pub")]
        public IActionResult ReqPub([FromQuery] string appId)
        {
            if(string.IsNullOrWhiteSpace(appId)) return BadRequest("empty appId");

            var publicKey = RSAKeyMapping.GetServerPublicKeyByAppId(appId);
            if(string.IsNullOrWhiteSpace(publicKey)) return BadRequest("invalid appId");

            return Ok(new { data = publicKey });
        }

        [HttpPost("set-pub")]
        public IActionResult SetPub([FromHeader] string appId, [FromBody] RequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.EncParm)) return BadRequest();

            var rsaKey = RSAKeyMapping.GetByAppId(appId);
            if (rsaKey == null) return BadRequest();

            // TODO
            var clientPubBytes = Convert.FromBase64String(dto.EncParm).AsSpan();
            List<byte> decBytes = new();
            for (int i = 0; i < clientPubBytes.Length; i += 250)
            {
                var bytes = clientPubBytes.Slice(i, Math.Min(250, clientPubBytes.Length - i)).ToArray();
                decBytes.AddRange(EncryptProvider.RSAEncrypt(rsaKey.ServerPrivateKey, bytes, RSAEncryptionPadding.Pkcs1, true));
            }

            //var decBytes = EncryptProvider.RSADecrypt(rsaKey.ServerPrivateKey, bytes, RSAEncryptionPadding.Pkcs1, true);

            var dec = System.Text.Encoding.UTF8.GetString(decBytes.ToArray());

            RSAKeyMapping.SetClientPublicKeyByAppId(appId, dec);

            var aesKey = AesKeyMapping.GetAesByAppId(appId);

            if (string.IsNullOrWhiteSpace(aesKey))
            {
                aesKey = AesKeyMapping.GenAesKey();
                AesKeyMapping.SetAes(appId, aesKey);
            }

            var encAesKey = EncryptProvider.RSAEncrypt(dec, aesKey, RSAEncryptionPadding.Pkcs1, true);

            return Ok(new { data = encAesKey });
        }
    }
}