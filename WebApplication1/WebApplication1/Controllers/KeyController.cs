using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class KeyController(Utils.Redis redis) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody]UserModel user)
    {
        if (string.IsNullOrEmpty(user.Name))
        {
            return Content("Invalid username");
        }
        var uuid = Guid.NewGuid().ToString();
        redis.SetString(user.Name, uuid);
        return Content(uuid);
    }
    [HttpPost]
    public IActionResult Delete([FromBody]UserModel user)
    {
        var result= redis.DeleteKey(user.Name);
        if (result)
        {
            return Content("Key deleted");
        }else{
            return Content("Key not found");
        }
        
    }
    [HttpPost]
    public IActionResult GetKey([FromBody]UserModel user)
    {
        var keyResult= redis.GetString(user.Name);
        return Content(keyResult);
    }
}