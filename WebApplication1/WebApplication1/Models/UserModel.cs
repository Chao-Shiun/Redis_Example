using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;

[BindProperties]
public class UserModel
{
    public string? Name { get; set; }
}