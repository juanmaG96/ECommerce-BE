using Ecommerce;
using Ecommerce.Config;
using Ecommerce.Mapper;
using Ecommerce.Models;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Services.Auth;
using Ecommerce.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la Base de datos con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configuracion de servicios

// Configuracion d e JWT
builder.Services.AddAuthentication(config  =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false; // Cambiar a true en producción
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"])),
        ValidateIssuer = false, // Puedes habilitar esto si tienes un emisor específico
        ValidateAudience = false, // Puedes habilitar esto si tienes una audiencia específica
        ClockSkew = TimeSpan.Zero // Para evitar problemas de tiempo de expiración
    };
});

//Inyecciones de dependencias
builder.Services.AddScoped<IUsuarioRepo, UsuarioRepository>();
//builder.Services.AddScoped<IPedidoRepo, PedidoRepository>();
builder.Services.AddScoped<IProductoRepo, ProductoRepository>();
//builder.Services.AddScoped<ICarritoRepo, CarritoRepository>();
//builder.Services.AddScoped<IDetallePedidoRepo, DetallePedidoRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();


// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyHeader());
});


// Configuracion de JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para que la sesión funcione en entornos sin consentimiento de cookies
});

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
});

var app = builder.Build();
// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1"));
}

app.UseSession();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

// Seed inicial para el administrador

var configuration = app.Services.GetRequiredService<IConfiguration>(); // Leer configuración
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    var jwtService = services.GetRequiredService<IJwtService>();

    var adminEmail = configuration["AdminSettings:Email"];
    var adminPassword = configuration["AdminSettings:Password"];

    await context.Database.EnsureCreatedAsync();

    var adminExists = await context.Usuario.AnyAsync(u => u.Rol == RolUsuario.Admin);
    if (!adminExists)
    {
        var nuevoAdmin = new Usuario
        {
            Nombre = "Administrador",
            CorreoElectronico = adminEmail,
            Rol = RolUsuario.Admin,
            PasswordHash = await jwtService.HashPasswordAsync(adminPassword)
        };
        await context.Usuario.AddAsync(nuevoAdmin);
        await context.SaveChangesAsync();
    }
}

app.UseAuthorization();
app.MapControllers();

app.Run();