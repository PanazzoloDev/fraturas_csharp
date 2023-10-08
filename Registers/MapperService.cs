using fraturas_csharp.Registers;
using AutoMapper;

namespace fraturas_csharp.MapperService
{
    public interface IMapperService
    {
        T Map<T>(object source) where T : class;
    }
    public class MapperService : IMapperService
    {
        private readonly IMapper _autoMapper;
        public MapperService()
        {
            _autoMapper = AutoMapperRegisters.Register();
        }
        public T Map<T>(object source) where T : class
        {
            return _autoMapper.Map<T>(source);
        }
    }
}