using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using System.Security.Cryptography;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BizController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromHeader] string appId, [FromBody] RequestDto dto)
        {
            if(string.IsNullOrWhiteSpace(appId)) return BadRequest("invalid appId");
            if (dto == null 
                || string.IsNullOrWhiteSpace(dto.EP)
                || string.IsNullOrWhiteSpace(dto.EAK)) return BadRequest("invalid param");

            var rsaKey = RSAKeyMapping.GetByAppId(appId);
            if (rsaKey == null) return BadRequest("invalid appId");

            var decAesKey = EncryptProvider.RSADecrypt(rsaKey.PrivateKey, dto.EAK, RSAEncryptionPadding.Pkcs1, true);

            var decData = EncryptProvider.AESDecrypt(dto.EP, decAesKey);

            return Ok($"Hello, {decData}");
        }
    }
}