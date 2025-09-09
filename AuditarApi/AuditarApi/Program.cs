using Application.Services.SeguridadServices;
using Dominio.Data;
using Dominio.ModuloConfiguracion.Repositorio;
using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionAuditar")));

#region Seguridad
builder.Services.AddScoped<SeguridadServices>();

    #region Usuario 
    builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<PasswordService>();
    #endregion

    #region Rol
    builder.Services.AddScoped<IRolRepository, RolRepository>();
    builder.Services.AddScoped<SeguridadServices>();
    #endregion

    #region RolOperacionAccion
    builder.Services.AddScoped<IRolOperacionAccionRepository, RolOperacionAccionRepository>();
#endregion

#endregion

#region Configuracion
builder.Services.AddScoped<ConfiguracionServices>();

    #region Menu
    builder.Services.AddScoped<IMenuRepository, MenuRepository>();
    #endregion

    #region MenuRol
    builder.Services.AddScoped<IMenuRolRepository, MenuRolRepository>();
    #endregion

    #region Accion
    builder.Services.AddScoped<IAccionRepository, AccionRepository>();
    #endregion

    #region Servicio
    builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
    #endregion
#endregion

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

