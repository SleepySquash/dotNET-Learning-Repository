using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(long id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> FindOneAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression);
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task RemoveAsync(User entity);
    }
}