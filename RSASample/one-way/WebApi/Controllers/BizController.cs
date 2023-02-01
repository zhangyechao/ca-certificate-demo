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
            if(string.IsNullOrWhiteSpace(appId)) return BadRequest();
            if (dto == null || string.IsNullOrWhiteSpace(dto.EncParm)) return BadRequest();

            var aesKey = AesKeyMapping.GetAesByAppId(appId);
            if (string.IsNullOrWhiteSpace(aesKey)) return BadRequest();

            var dec = EncryptProvider.AESDecrypt(dto.EncParm, aesKey);

            return Ok($"Hello, {dec}");
        }
    }
}