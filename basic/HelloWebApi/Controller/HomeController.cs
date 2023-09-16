using HelloWebApi.Modals;
using Microsoft.AspNetCore.Mvc;

namespace HelloWebApi.Controller
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ResponseModel GetMessage()
        {
            return new ResponseModel()
            {
                message = "Apitest",
                statuskode = 12
            };
        }

    }
}
