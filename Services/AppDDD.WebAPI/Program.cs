using AppDDD.DAL;
using AppDDD.DAL.Context;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connection_type = builder.Configuration["Database"];
var connection_string = builder.Configuration.GetConnectionString(connection_type);

switch (connection_type)
{
    case "SqlServer":
        services.AddDbContext<AppDB>(opt => opt.UseSqlServer(connection_string, o => o.MigrationsAssembly("AppDDD.DAL.MSSQLServer")));
        break;
    case "Sqlite":
        services.AddDbContext<AppDB>(opt => opt.UseSqlite(connection_string, o => o.MigrationsAssembly("AppDDD.DAL.Sqlite")));
        break;
}

services.AddTransient<AppDbInitializer>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbInitializer>();
    await initializer.InitializeAsync(RemoveBefore: false);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
