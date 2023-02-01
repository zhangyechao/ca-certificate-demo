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

            var publicKey = RSAKeyMapping.GetPublicKeyByAppId(appId);
            if(string.IsNullOrWhiteSpace(publicKey)) return BadRequest("invalid appId");

            return Ok(new { data = publicKey });
        }

        [HttpPost("set-key")]
        public IActionResult SetKey([FromHeader] string appId, [FromBody] RequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.EncParm)) return BadRequest();

            var rsaKey = RSAKeyMapping.GetByAppId(appId);
            if (rsaKey == null) return BadRequest();

            var dec = EncryptProvider.RSADecrypt(rsaKey.PrivateKey, dto.EncParm, RSAEncryptionPadding.Pkcs1, true);

            AesKeyMapping.SetAes(appId, dec);

            return Ok();
        }
    }
}