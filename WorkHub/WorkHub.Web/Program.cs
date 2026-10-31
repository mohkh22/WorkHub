using Microsoft.EntityFrameworkCore;
using WorkHub.Application.Interfaces;
using WorkHub.Application.Services;
using WorkHub.Infrastructure.Data;
using WorkHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=spaces}/{action=Index}/{id?}");


app.Run();
