using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IConditionsRepository
    {
        Task<Condition> GetAsync(long id);
        Task<IEnumerable<Condition>> GetAllAsync();
        Task<Condition> FindOneAsync(Expression<Func<Condition, bool>> expression);
        Task<IEnumerable<Condition>> FindAsync(Expression<Func<Condition, bool>> expression);
        Task AddAsync(Condition entity);
        Task UpdateAsync(Condition entity);
        Task RemoveAsync(Condition entity);
    }
}