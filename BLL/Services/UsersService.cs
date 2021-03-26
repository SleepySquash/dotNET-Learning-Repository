using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public async Task CreateAsync(User user)
        {
            var userDb = await _usersRepository.FindAsync(u => u.Phone == user.Phone);
            if (userDb.Any()) throw new InvalidOperationException("Phone must be unique");
            await _usersRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User modifiedUser)
        {
            await _usersRepository.UpdateAsync(modifiedUser);
        }

        public async Task RemoveAsync(long id)
        {
            var user = new User {Id = id};
            await _usersRepository.RemoveAsync(user);
        }

        public async Task RemoveAsync(string phone)
        {
            var user = await _usersRepository.FindOneAsync(u => u.Phone == phone);
            await _usersRepository.RemoveAsync(user);
        }
        
        public async Task RemoveAsync(User user)
        {
            await _usersRepository.RemoveAsync(user);
        }

        public async Task<User> GetAsync(long id)
        {
            return await _usersRepository.GetAsync(id);
        }

        public async Task<User> GetAsync(string phone)
        {
            return await _usersRepository.FindOneAsync(u => u.Phone == phone);
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
        {
            return await _usersRepository.FindAsync(expression);
        }

        public async Task<IEnumerable<User>> AllAsync()
        {
            return await _usersRepository.GetAllAsync();
        }

        public async Task<User> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await _usersRepository.FindOneAsync(expression);
        }
    }
}