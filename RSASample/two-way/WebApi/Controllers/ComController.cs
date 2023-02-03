using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComController : ControllerBase
    {
        [HttpGet("req-pub")]
        public IActionResult ReqPub([FromQuery] string appId)
        {
            if(string.IsNullOrWhiteSpace(appId)) return BadRequest("invalid param");

            var publicKey = RSAKeyMapping.GetServerPublicKeyByAppId(appId);
            if(string.IsNullOrWhiteSpace(publicKey)) return BadRequest("invalid appId");

            return Ok(new { data = publicKey });
        }

        [HttpPost("set-pub")]
        public IActionResult SetPub([FromHeader] string appId, [FromBody] string cpk)
        {
            if (string.IsNullOrWhiteSpace(cpk)) return BadRequest("invalid param");

            var rsaKey = RSAKeyMapping.GetByAppId(appId);
            if (rsaKey == null) return BadRequest("invalid appId");

            RSAKeyMapping.SetClientPublicKeyByAppId(appId, cpk);

            var aesKey = AesKeyMapping.GetAesByAppId(appId);

            if (string.IsNullOrWhiteSpace(aesKey))
            {
                aesKey = AesKeyMapping.GenAesKey();
                AesKeyMapping.SetAes(appId, aesKey);
            }

            var encAesKey = EncryptProvider.RSAEncrypt(cpk, Encoding.UTF8.GetBytes(aesKey), RSAEncryptionPadding.Pkcs1, true);

            return Ok(new { data = Convert.ToBase64String(encAesKey) });
        }
    }
}