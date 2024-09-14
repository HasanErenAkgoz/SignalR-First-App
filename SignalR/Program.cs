using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SignalR.Business;
using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// CORS ayarlarý
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed(origin => true);
    });
});

// Swagger servislerini ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "SignalR API documentation"
    });
});

builder.Services.AddTransient<MyBusiness>();
builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

// Geliþtirme ortamý için hata sayfasý
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger UI ana sayfa olarak ayarlamak için
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseCors();
app.MapHub<MyHub>("/myhub");
app.MapHub<MessageHub>("/messagehub");
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.Run();
