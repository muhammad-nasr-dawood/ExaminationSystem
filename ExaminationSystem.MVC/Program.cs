using ExaminationSystem.Core;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.EF.Repositories;
using ExaminationSystem.MVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register services in the Dependency Injection container (DIC)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // new object will be created for each request
builder.Services.AddScoped<IStudentRepo, StudentRepo>(); // new object will be created for each request 

builder.Services.AddAutoMapper(typeof(Program)); // regiseration for auto mapper (uses refelection)
builder.Services.AddScoped<IStudentService, StudentService>(); // this is the only layer that can deal with the controller directly (any other dirty work like auto-mapping etc will be within it)


builder.Services.AddDbContext<ExaminationDBContext>(
            options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped // will create a new object for each request
        ); // if you didn't pass this callback function it will use the default constructor of the DbContext and will use the connection string from the OnConfiguring method in the ITIDBContext class
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
