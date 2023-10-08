using System.Text;
using fraturas_csharp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace APIWorkflow.Registers
{
    public static class AuthorizationRegister
    {
        public static void Register(WebApplicationBuilder builder)
        {
            string tokenKey = Environment.GetEnvironmentVariable("tokenKey", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(tokenKey))
                throw new NullReferenceException("Token nulo!");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "FraturaAPI",
                    ValidAudience = "Anônima",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey))
                };
            });
        }
    }
}