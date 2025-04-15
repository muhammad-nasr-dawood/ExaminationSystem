using ExaminationSystem.Core;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.EF.Repositories;
using ExaminationSystem.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IDepartmentService, DeparmentService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<ILocationRepo, LocationRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<IPasswordService, PasswordService>(); // singleton no need for more than one object for this servcie since it's a stateless utility service and there's no shared state or data (and our code will be also loosly coupled better than using static class which will make our code tightly coupled)

builder.Services.AddScoped<IBaseRepo<Staff>, BaseRepo<Staff>>(); // staff repo using the generic repo
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddDbContext<ExaminationDBContext>(
            options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped // will create a new object for each request
        ); // if you didn't pass this callback function it will use the default constructor of the DbContext and will use the connection string from the OnConfiguring method in the ITIDBContext class


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(
				options =>
				{
				  options.LoginPath = "/auth/logincover"; // the path to the login page
				  options.LogoutPath = "/auth/Logout"; // the path to the logout page
				  options.AccessDeniedPath = "/Account/AccessDenied"; // the path to the access denied page
				  options.SlidingExpiration = true; // if you set it to true it will reset the expiration time each time the user makes a request
				}
			);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
