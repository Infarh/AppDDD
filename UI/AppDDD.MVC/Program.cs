using AppDDD.Domain.Entities;
using AppDDD.Interfaces;
using AppDDD.WebAPI.Clients.Employees;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();

services.AddHttpClient("AppDDDWebAPI", client => client.BaseAddress = new(builder.Configuration["WebAPI"]))
   .AddTypedClient<IRepositoryAsync<Employee>, EmployeesClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
