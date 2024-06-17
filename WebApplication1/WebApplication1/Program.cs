using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var redisHost = builder.Configuration.GetSection("Redis:ConnectionString").Value;
var redisPort = int.Parse(builder.Configuration.GetSection("Redis:Port").Value);
var redisPassword = builder.Configuration.GetSection("Redis:Password").Value;


builder.Services.AddSingleton(new WebApplication1.Utils.Redis(redisHost,redisPort,redisPassword));
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();