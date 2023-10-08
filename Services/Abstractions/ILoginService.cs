using System.Collections;
using fraturas_csharp.Data.DTOs;
using fraturas_csharp.Models;

namespace fraturas_csharp.Services
{
    public interface ILoginService
    {
        public IResponseMessages Authenticate(string username, string password);

    }
}