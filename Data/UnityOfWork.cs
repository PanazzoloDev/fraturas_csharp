
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace fraturas_csharp.Data
{
    public interface IUnityOfWork
    {
        int SaveLocal();
        bool SendToDb();
    }

    public class UnityOfWork : IUnityOfWork
    {
        private DbContext instance;
        private IDbContextTransaction transaction;
        public UnityOfWork(IServiceProvider service)
        {
            var context = service.GetService<FraturasContext>();
            this.instance = context;
            this.transaction = instance.Database.BeginTransaction();
        }
        public int SaveLocal()
        {
            return instance.SaveChanges();
        }
        public bool SendToDb()
        {
            int affectedRows = instance.SaveChanges();
            transaction.Commit();
            return affectedRows > 0;
        }
    }
}