using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();
// app.UseMiddleware<TestMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


// --------- Datenbank mit Probedaten befüllen
// Exception hier drin wird nicht gecatched von ExceptionMiddleware, weil kein HTTP Request ausführt wurde
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;

try{
    var context = service.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch(Exception e){
    var logger = service.GetService<ILogger<Program>>();
    logger.LogError(e, "An error occured during migration");
}
// ---------


app.Run();
