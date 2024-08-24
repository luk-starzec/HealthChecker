using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        [HttpGet]
        public string GetData()
        {
            return $"Test data generated at {DateTime.Now}";
        }
    }
}
