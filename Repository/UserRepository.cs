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
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(RapidPayContext context)
            : base(context) { }

        public async Task<User?> GetByName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
        }
    }
}
