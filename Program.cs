using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MJRPAdmin.DBContext;
using MJRPAdmin.Mapping;
using MJRPAdmin.Service;
using MJRPAdmin.Service.interfaces;
using MJRPAdmin.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromHours(24);
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false)
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//db connection string
builder.Services.AddDbContext<mjrpContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("dbCon"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));
});

//builder.Services.AddScoped<IFacultyService,IFacultyService>();
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}");
        //pattern: "{controller=Auth}/{action=Login}/{id?}");
        pattern: "{controller=Home}/{action=home}/{id?}");

app.Run();
