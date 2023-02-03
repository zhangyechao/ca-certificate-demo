using Microsoft.AspNetCore.Mvc;

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
    }
}