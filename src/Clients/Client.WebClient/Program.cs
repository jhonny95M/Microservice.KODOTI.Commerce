using Api.Gateway.WebClient.Proxy;
using Api.Gateway.WebClient.Proxy.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new ApiGatewayUrl(builder.Configuration.GetValue<string>("ApiGatewayurl")))
    .AddHttpContextAccessor();
builder.Services.AddHttpClient<IOrderProxy,OrderProxy>();
builder.Services.AddHttpClient<IProductProxy,ProductProxy>();
builder.Services.AddHttpClient<IClientProxy,ClientProxy>();
builder.Services.AddRazorPages(o => o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();  
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
