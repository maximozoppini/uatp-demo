using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T, Int> where T : BaseEntity
    { 

        Task<T> GetById(int id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

    }
}
