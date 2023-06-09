using Microsoft.EntityFrameworkCore;
using SAP_1.Models;
using SAP_1.Services;
using SAP_1.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AcademicoContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("AcademicoDatabase")));


builder.Services.AddScoped<IDepartamentoService, DBDepartamentosContext>();
builder.Services.AddScoped<IEmpregadoService, DBEmpregadosContext>();
builder.Services.AddScoped<ICursoService, DbCursosContext>();
builder.Services.AddScoped<ICursoOferecidoService, DbCursosOferecidosContext>();
builder.Services.AddScoped<IMatriculaService, DBMatriculasContext>();
builder.Services.AddScoped<IHistoricoService, DbHistoricoContext>();

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
