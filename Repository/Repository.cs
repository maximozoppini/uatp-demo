using Entities;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Repository<T, Int> : IRepository<T, Int> where T : BaseEntity
    {
        protected readonly RapidPayContext _context;
        protected Repository(RapidPayContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(true);
            return entity;
        }
        public async Task<T> GetById(int id) => await _context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
