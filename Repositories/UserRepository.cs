using fraturas_csharp.Data;
using fraturas_csharp.Entities;

namespace fraturas_csharp.Repositories;
public interface IUserRepository : IRepository<User>
{
    // Métodos específicos para a entidade City, se necessário
}
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(FraturasContext context) : base(context){}
}