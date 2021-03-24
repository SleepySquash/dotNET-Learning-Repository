using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUsersService
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User modifiedUser);
        Task RemoveAsync(long id);
        Task RemoveAsync(string phone);
        Task RemoveAsync(User user);
        Task<User> GetAsync(long id);
        Task<User> GetAsync(string phone);
        Task<User> AnyAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> AllAsync();
    }
}