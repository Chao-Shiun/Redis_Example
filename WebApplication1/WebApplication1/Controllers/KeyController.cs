using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class KeyController(Utils.Redis redis) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody]string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return Content("Invalid username");
        }
        var uuid = Guid.NewGuid().ToString();
        redis.SetString(userName, uuid);
        return Content(uuid);
    }
    [HttpPost]
    public IActionResult Delete([FromBody]string key)
    {
        var result= redis.DeleteKey(key);
        if (result)
        {
            return Content("Key deleted");
        }else{
            return Content("Key not found");
        }
        
    }
    [HttpPost]
    public IActionResult GetKey([FromBody]string userName)
    {
        var keyResult= redis.GetString(userName);
        return Content(keyResult);
    }
}