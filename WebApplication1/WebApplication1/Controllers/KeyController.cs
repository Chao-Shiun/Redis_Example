using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class KeyController(Utils.Redis redis) : Controller
{
    [HttpPost]
    public IActionResult Create()
    {
        var uuid = Guid.NewGuid().ToString();
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
    public IActionResult GetKey([FromBody]string key)
    {
        var keyResult= redis.GetString(key);
        return Content(keyResult);
    }
}