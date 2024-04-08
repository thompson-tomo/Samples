using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CloudFoundryJwtAuthentication.Controllers;

[Route("api/JwtAuthorization")]
public class JwtAuthorizationController : Controller
{
    // GET api/values
    [HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "test.group")]
    public IEnumerable<string> Get()
    {
        return ["value1", "value2"];
    }
}