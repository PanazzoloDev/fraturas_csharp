using fraturas_csharp.Models;
using fraturas_csharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fraturas_csharp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult GetPaged(int pageNumber,int pageSize)
    {
        return Ok(_service.GetPaged(pageNumber,pageSize));
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        return Ok(_service.GetById(id));
    }
    
    [HttpPost]
    public ActionResult Create(UserNewModel model)
    {
        return Ok(_service.Create(model));
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, UserUpdateModel model)
    {
        return Ok(_service.Update(id, model));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok(_service.Delete(id));
    }
}
