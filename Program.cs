using System.Text;
using APIWorkflow.Registers;
using fraturas_csharp.Data;
using fraturas_csharp.MapperService;
using fraturas_csharp.Registers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
string? stringDeConexao = builder.Configuration.GetConnectionString("DefaultConnection");

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddDbContext<FraturasContext>(opt => opt.UseNpgsql(stringDeConexao));

// Service, Repository e Authentication
RepositoryRegisters.Register(builder);
AppServiceRegisters.Register(builder);
AuthorizationRegister.Register(builder);

// AutoMapper
builder.Services.AddSingleton<IMapperService, MapperService>();
// UnityOfWork
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

// Swagger para authorization
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira o token JWT no formato 'Bearer {seu_token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
