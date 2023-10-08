using System.Collections;
using fraturas_csharp.Data.DTOs;

namespace fraturas_csharp.Services
{

    public interface IBaseService<N, U, V>
        where N : class
        where U : class
    {
        public IResponseMessages GetPaged(int pageSize, int pageNumber);
        public IResponseMessages GetById(int id);
        public IResponseMessages Create(N model);
        public IResponseMessages Update(int id, U model);
        public bool Delete(int id);
    }

}