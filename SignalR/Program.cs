using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .SetIsOriginAllowed( origin => true);
    });
});

builder.Services.AddSignalR();
var app = builder.Build();

// Geliþtirme ortamý için hata sayfasý
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseCors();
app.MapHub<MyHub>("/myhub");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.Run();

