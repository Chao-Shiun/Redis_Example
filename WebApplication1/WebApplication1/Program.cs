using StackExchange.Redis;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);
var redisHost = builder.Configuration.GetSection("Redis:Host").Value;
var redisPort = int.Parse(builder.Configuration.GetSection("Redis:Port").Value);
var redisPassword = builder.Configuration.GetSection("Redis:Password").Value;

Console.WriteLine("redisHost:"+redisHost);
Console.WriteLine("redisPort:"+redisPort);
Console.WriteLine("redisPassword:"+redisPassword);


builder.Services.AddSingleton(new WebApplication1.Utils.Redis(redisHost,redisPort,redisPassword));
// Add services to the container.
builder.Services.AddControllersWithViews();

// 創建ConfigurationOptions對象
var configurationOptions = new ConfigurationOptions
{
    EndPoints = { { redisHost, redisPort } },
    Password = redisPassword,
    // 可以添加其他配置選項
};

Console.WriteLine(configurationOptions.ToString());

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = configurationOptions;
    options.InstanceName = "SampleInstance:";
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();