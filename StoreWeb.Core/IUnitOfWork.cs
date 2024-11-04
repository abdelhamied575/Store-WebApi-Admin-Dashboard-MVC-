using StoreWeb.Core.Entities;
using StoreWeb.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();

        // Create Repository <T> And Return It
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;


    }
}
