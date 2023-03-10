using Assignment_Intership.Contracts;
using Assignment_Intership.Data;
using Assignment_Intership.Data.Repositories;
using Assignment_Intership.MapProfiles;
using Assignment_Intership.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddScoped<IApplicationRepository, ApplicationRepository>()
    .AddScoped<IEmlpoyeeService, EmployeeService>()
    .AddScoped<ITaskService, TaskService>();


builder.Services.AddAutoMapper(typeof(EmployeeProfile),
    typeof(TaskProfile));

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
