using Application.Services.SeguridadServices;
using Dominio.Data;
using Dominio.ModuloConfiguracion.Repositorio;
using Dominio.ModuloPages.Repositorio;
using Dominio.ModuloSeguridad.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration["Jwt:Key"] ?? "1234567890ABCDEF1234567890ABCDEF";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "AuditarApi";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add services to the container.
//

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

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

#region pagues
builder.Services.AddScoped<PagesServices>();
    #region Auditar
    builder.Services.AddScoped<IAuditarPaginaRepository, AuditarPaginaRepository>();
    #endregion
    #region AuditarLog
    builder.Services.AddScoped<IAuditarLogRepository, AuditarLogRepository>();
    #endregion
#endregion

#region CORS
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
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

