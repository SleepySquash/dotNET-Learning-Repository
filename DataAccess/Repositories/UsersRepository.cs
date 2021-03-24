using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        protected readonly ParkinsonDbContext _context;
        public UsersRepository(ParkinsonDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> FindOneAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.FirstOrDefaultAsync(expression);
        }
        
        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Where(expression).ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified; 
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}