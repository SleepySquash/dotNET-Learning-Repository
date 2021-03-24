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
    public class ConditionsRepository : IConditionsRepository
    {
        protected readonly ParkinsonDbContext _context;
        public ConditionsRepository(ParkinsonDbContext context)
        {
            _context = context;
        }

        public async Task<Condition> GetAsync(long id)
        {
            return await _context.Conditions.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Condition>> GetAllAsync()
        {
            return await _context.Conditions.ToListAsync();
        }

        public async Task<Condition> FindOneAsync(Expression<Func<Condition, bool>> expression)
        {
            return await _context.Conditions.FirstOrDefaultAsync(expression);
        }
        
        public async Task<IEnumerable<Condition>> FindAsync(Expression<Func<Condition, bool>> expression)
        {
            return await _context.Conditions.Where(expression).ToListAsync();
        }

        public async Task AddAsync(Condition entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Condition entity)
        {
            _context.Entry(entity).State = EntityState.Modified; 
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Condition entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}