using CI_platfom.Entity.Data;

using CI_platform.Repository.Interface;
using CI_platform.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CiPlatformContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
//builder.Services.AddScoped<IMissionDetail, MissionDetail>();
//builder.Services.AddScoped<IMissionList, MissionList>();
//builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<IHomeLandingRepository, HomeLandingRepository>();
builder.Services.AddScoped<IMissionLandingRepository, MissionLandingRepository>();
builder.Services.AddScoped<IStoryHomeLandingRepository, StoryHomeLandingRepository>();
//builder.Services.AddScoped<IStoryLandingRepository, StoryLandingRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSession();
builder.Services.AddMemoryCache();

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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
