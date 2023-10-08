using fraturas_csharp.Services;

namespace fraturas_csharp.Registers
{
    public static class AppServiceRegisters
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ILoginService,LoginService>();
        }
    }
}