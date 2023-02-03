using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;

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
            if (dto == null || string.IsNullOrWhiteSpace(dto.EncParm)) return BadRequest("invalid param");

            var aesKey = AesKeyMapping.GetAesByAppId(appId);
            if (string.IsNullOrWhiteSpace(aesKey)) return BadRequest("invalid appId");

            // normally, this is a json string,
            // after decrypting, just deserialize it to your object
            var dec = EncryptProvider.AESDecrypt(dto.EncParm, aesKey);

            return Ok($"Hello, {dec}");
        }
    }
}