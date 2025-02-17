using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Repositories;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);
OracleConfiguration.SqlNetAllowedLogonVersionClient = OracleAllowedLogonVersionClient.Version11;
// Add services to the container.
//builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleados>();
//string connectionString =
//    builder.Configuration.GetConnectionString("SqlHospital");
//builder.Services.AddDbContext<HospitalContext>
//    (options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosOracle>();
string connectionString =
    builder.Configuration.GetConnectionString("OracleHospital");
builder.Services.AddDbContext<HospitalContext>
    (options => options.UseOracle(connectionString
    , options => options.UseOracleSQLCompatibility
    (OracleSQLCompatibility.DatabaseVersion19)));


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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
