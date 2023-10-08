using fraturas_csharp.Repositories;

namespace APIWorkflow.Registers
{
    public static class RepositoryRegisters
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository,UserRepository>();
        }
    }
}