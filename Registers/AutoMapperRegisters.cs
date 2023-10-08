using fraturas_csharp.Models;
using AutoMapper;
using fraturas_csharp.Entities;

namespace fraturas_csharp.Registers
{
    public static class AutoMapperRegisters
    {                  
        public static IMapper Register()
        {
            var configuration = new MapperConfiguration(cfg => 
            {
                UpdatedModelToEntity(ref cfg);
                NewModelToEntity(ref cfg);
                ViewModelToEntity(ref cfg);
            });
            return configuration.CreateMapper();
        }

        private static void UpdatedModelToEntity(ref IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserUpdateModel, User>();
        
        }
        private static void NewModelToEntity(ref IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserNewModel, User>();
        }

        private static void ViewModelToEntity(ref IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserViewModel, User>();
        }
    }
}