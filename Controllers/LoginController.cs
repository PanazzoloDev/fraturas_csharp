
using fraturas_csharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fraturas_csharp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public ActionResult Login(string username, string password)
    {
        return Ok(_service.Authenticate(username, password));
    }
}
