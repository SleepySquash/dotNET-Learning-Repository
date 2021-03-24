using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IConditionsService
    {
        Task CreateAsync(Condition condition);
        Task UpdateAsync(Condition modifiedCondition);
        Task RemoveAsync(long id);
        Task RemoveAsync(Condition condition);
        Task<Condition> GetAsync(long id);
        Task<IEnumerable<Condition>> FindAsync(Expression<Func<Condition, bool>> expression);
        Task<IEnumerable<Condition>> AllOfUserAsync(long userId);
        Task<IEnumerable<Condition>> AllAsync();
    }
}