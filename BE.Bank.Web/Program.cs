using BE.Bank.Web.Data.Context;
using BE.Bank.Web.Data.Interfaces;
using BE.Bank.Web.Data.Repositories;
using BE.Bank.Web.Data.UnitOfWork;
using BE.Bank.Web.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Servisleri yapýlandýr
builder.Services.AddDbContext<BankContext>(opt =>
{
    opt.UseSqlServer("server=.\\SQLEXPRESS; database=BankDb; integrated security=True; TrustServerCertificate=True");
});
//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IUow, Uow>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountMapper, AccountMapper>();
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Ortam kontrolü
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Statik dosyalar
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/node_modules"
});

app.UseRouting();

// Endpoint tanýmý
app.MapDefaultControllerRoute();

app.Run();
