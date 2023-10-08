using System.Net;
using fraturas_csharp.Data;
using fraturas_csharp.Data.DTOs;
using fraturas_csharp.Entities;
using fraturas_csharp.MapperService;
using fraturas_csharp.Models;
using fraturas_csharp.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace fraturas_csharp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IUnityOfWork _uow;
        private readonly IMapperService _mapper;
        public UserService(IUserRepository users, IUnityOfWork uow, IMapperService mapper)
        {
            _users = users;
            _uow = uow;
            _mapper = mapper;
        }

        public IResponseMessages GetPaged(int pageNumber, int pageSize)
        {
            var data = _users.GetAll()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Password = x.Password,
            })
            .ToList();

            if (data.Count == 0)
                return new ResponseMessages("Nenhum registro encontrado!", HttpStatusCode.NoContent);

            return new ResponseMessages(
                "Registros atualizados com sucesso!",
                new
                {
                    pageNumber,
                    pageSize,
                    data,
                    totalRows = data.Count
                }
            );
        }
        public IResponseMessages GetById(int id)
        {
            var data = _users.Where(x => x.Id == id)
            .Select(x => new
            {
                x.Id,
                x.Name,
            })
            .FirstOrDefault();

            if (data == null)
                return new ResponseMessages("Registro não encontrado!", HttpStatusCode.NoContent);

            return new ResponseMessages(data);
        }
        public IResponseMessages Create(UserNewModel entity)
        {
            var user = _mapper.Map<User>(entity);

            _users.Add(user);
            var created = _uow.SendToDb();

            if (!created)
                return new ResponseMessages("Problemas ao salvar o registro", HttpStatusCode.InternalServerError);

            return new ResponseMessages(
                new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                }
            );
        }
        public IResponseMessages Update(int id, UserUpdateModel entity)
        {
            var user = _mapper.Map<User>(entity);
            user.Id = id;
            _users.Update(user);
            var updated = _uow.SendToDb();

            if (!updated)
                return new ResponseMessages("Problemas ao atualizar o registro!", HttpStatusCode.InternalServerError);
            return new ResponseMessages(
                new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                }
            );
        }
        public bool Delete(int id)
        {
            var user = _users.GetById(id);
            if (user != null)
            {
                _users.Delete(user);
                return _uow.SendToDb();
            }
            return false;
        }
    }
}
