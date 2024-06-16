using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class KeyController : Controller
{
    [HttpPost]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Delete()
    {
        return View();
    }
    [HttpPost]
    public IActionResult GetKey()
    {
        return View();
    }
}