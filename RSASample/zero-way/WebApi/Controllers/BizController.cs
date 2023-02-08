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
        public async Task<IActionResult> Post([FromHeader] string appId)
        {
            if (string.IsNullOrWhiteSpace(appId)) return BadRequest("invalid appId");

            var data = await new StreamReader(Request.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(data)) return BadRequest("invalid param");

            var rsaKey = RSAKeyMapping.GetByAppId(appId);
            if (rsaKey == null) return BadRequest("invalid appId");

            // normally, this is a json string,
            // after decrypting, just deserialize it to your object
            var decData = EncryptProvider.RSADecrypt(rsaKey.ServerPrivateKey, Convert.FromBase64String(data), RSAEncryptionPadding.Pkcs1, true);

            return Ok($"Hello, {System.Text.Encoding.UTF8.GetString(decData)}");
        }
    }
}