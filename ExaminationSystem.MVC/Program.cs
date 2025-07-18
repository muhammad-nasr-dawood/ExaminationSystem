using AutoMapper;
using ExaminationSystem.Core;
using ExaminationSystem.Core.Helpers;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using ExaminationSystem.EF;
using ExaminationSystem.EF.Repositories;
using ExaminationSystem.MVC.IService;
using ExaminationSystem.MVC.MappingProfiles;
using ExaminationSystem.MVC.Services;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



// Add services to the container.
builder.Services.AddControllersWithViews();
// Register services in the Dependency Injection container (DIC)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // new object will be created for each request
													   //builder.Services.AddScoped<IStudentRepo, StudentRepo>(); // new object will be created for each request 
builder.Services.AddScoped<IBaseRepo<Student>, BaseRepo<Student>>(); builder.Services.AddScoped<IQuestionRepo, QuestionRepo>(); // new object will be created for each request

builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddAutoMapper(typeof(Program)); // regiseration for auto mapper (uses refelection)


builder.Services.AddAutoMapper(cfg =>
{
  cfg.AddProfile<GenericPoolStateProfile<ProcessedPoolsResult>>();
  cfg.AddProfile<GenericPoolStateProfile<ActivePoolsResult>>();

});


builder.Services.AddScoped<IStudentService, StudentService>(); // this is the only layer that can deal with the controller directly (any other dirty work like auto-mapping etc will be within it)
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IDepartmentService, DeparmentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITopicService,TopicService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IBranchRepo, BranchRepo>();
builder.Services.AddScoped<IStaffBranchManageRepo, StaffBranchManageRepo>();

builder.Services.AddScoped<IBaseRepo<Department>, BaseRepo<Department>>();
builder.Services.AddScoped<ILocationRepo, LocationRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBaseRepo<StaffBranchIntakeWorksFor>, BaseRepo<StaffBranchIntakeWorksFor>>();
builder.Services.AddScoped<IBaseRepo<Intake>, BaseRepo<Intake>>();



builder.Services.AddSingleton<IPasswordService, PasswordService>(); // singleton no need for more than one object for this servcie since it's a stateless utility service and there's no shared state or data (and our code will be also loosly coupled better than using static class which will make our code tightly coupled)
builder.Services.AddScoped<IImageService, ImageKitService>();

builder.Services.AddScoped<IBaseRepo<Staff>, BaseRepo<Staff>>(); // staff repo using the generic repo
builder.Services.AddScoped<IBaseRepo<Student>, BaseRepo<Student>>();
//builder.Services.AddScoped<IStudentRepo, StudentRepo>();

builder.Services.AddScoped<IBaseRepo<User>, BaseRepo<User>>();


builder.Services.AddScoped<IBaseRepo<Branch>, BaseRepo<Branch>>();
builder.Services.AddScoped<IBaseRepo<StudentIntakeBranchDepartmentStudy>, BaseRepo<StudentIntakeBranchDepartmentStudy>>();
builder.Services.AddScoped<IBaseRepo<Intake>, BaseRepo<Intake>>();
builder.Services.AddScoped<IBaseRepo<Department>, BaseRepo<Department>>();
builder.Services.AddScoped<IBaseRepo<Location>, BaseRepo<Location>>();
builder.Services.AddScoped<IBaseRepo<StaffBranchIntakeWorksFor>, BaseRepo<StaffBranchIntakeWorksFor>>();

builder.Services.AddScoped<IBaseRepo<StudentExamModel>, BaseRepo<StudentExamModel>>();

builder.Services.AddScoped<IStudentService, StudentService>(); // this is the only layer that can deal with the controller directly (any other dirty work like auto-mapping etc will be within it)

builder.Services.AddScoped<IBaseRepo<Location>, BaseRepo<Location>>();

builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IBaseRepo<StaffBranchIntakeDepartmentCourseTeach>, BaseRepo<StaffBranchIntakeDepartmentCourseTeach>>();
builder.Services.AddScoped<IBaseRepo<Course>, BaseRepo<Course>>();
builder.Services.AddScoped<IBaseRepo<ProfileImage>, BaseRepo<ProfileImage>>();
builder.Services.AddScoped<IBaseRepo<Topic>, BaseRepo<Topic>>();
builder.Services.AddScoped<IBaseRepo<ProfileImage>, BaseRepo<ProfileImage>>();  

builder.Services.AddTransient<IEmailService, EmailService>();


builder.Services.AddScoped<IPoolRepo,PoolRepo>();
builder.Services.AddScoped<IPoolService, PoolService>();

// Register ImageKit configuration
builder.Services.Configure<ImageKitSettings>(builder.Configuration.GetSection("ImageKit"));

// Alternative: Factory pattern for better thread safety
builder.Services.AddTransient<ImagekitClient>(provider =>
{
  var config = provider.GetRequiredService<IOptions<ImageKitSettings>>().Value;
  return new ImagekitClient(
	  config.PublicKey,
	  config.PrivateKey,
	  config.UrlEndpoint
  );
});


/*nasser*/
builder.Services.AddScoped<IImageKit, ExaminationSystem.MVC.Services.ImageKit>();

builder.Services.AddScoped<IUserClaimService, UserClaimService>();


builder.Services.AddDbContext<ExaminationDBContext>(
            options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped // will create a new object for each request
        ); // if you didn't pass this callback function it will use the default constructor of the DbContext and will use the connection string from the OnConfiguring method in the ITIDBContext class

/*nasser*/



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
