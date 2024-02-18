using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ContentResult Index() 
        {
            
            return base.Content("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>Document</title>\r\n  </head>\r\n  <body>\r\n    <h1>Đây là API của Nguyễn Khánh Minh Đăng</h1>\r\n    <div style=\"display: flex\">\r\n      <a href=\"/Swagger/index.html\">API Swagger</a>\r\n    </div>\r\n  </body>\r\n</html>\r\n", "text/html");
        }
    }
}
