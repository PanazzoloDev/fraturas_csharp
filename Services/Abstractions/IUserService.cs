using fraturas_csharp.Entities;
using fraturas_csharp.Models;

namespace fraturas_csharp.Services
{
    public interface IUserService: IBaseService<
        UserNewModel,
        UserUpdateModel,
        UserViewModel
    >
    {
        
    }
}