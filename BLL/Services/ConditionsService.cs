using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace BLL.Services
{
    namespace BLL.Services
    {
        public class ConditionsService : IConditionsService
        {
            private readonly IConditionsRepository _conditionsRepository;
            private readonly IUsersRepository _usersRepository;
            public ConditionsService(IConditionsRepository conditionsRepository, IUsersRepository usersRepository)
            {
                _conditionsRepository = conditionsRepository;
                _usersRepository = usersRepository;
            }
            
            public async Task CreateAsync(Condition condition)
            {
                var userFromDb = await _usersRepository.FindOneAsync(u => u.Id == condition.UserId);
                if (userFromDb == null) throw new InvalidOperationException("Not found");
                await _conditionsRepository.AddAsync(condition);
            }
    
            public async Task UpdateAsync(Condition modifiedCondition)
            {
                var userFromDb = await _usersRepository.FindOneAsync(u => u.Id == modifiedCondition.UserId);
                if (userFromDb == null) throw new InvalidOperationException("Not found");

                try { await _conditionsRepository.UpdateAsync(modifiedCondition); }
                catch (Exception e) { throw new InvalidOperationException("Not found"); }
            }
    
            public async Task RemoveAsync(long id)
            {
                var condition = new Condition {Id = id};
                await _conditionsRepository.RemoveAsync(condition);
            }

            public async Task RemoveAsync(Condition condition)
            {
                await _conditionsRepository.RemoveAsync(condition);
            }

            public async Task<Condition> GetAsync(long id)
            {
                return await _conditionsRepository.GetAsync(id);
            }

            public async Task<IEnumerable<Condition>> FindAsync(Expression<Func<Condition, bool>> expression)
            {
                return await _conditionsRepository.FindAsync(expression);
            }

            public async Task<IEnumerable<Condition>> AllOfUserAsync(long userId)
            {
                return await _conditionsRepository.FindAsync(c => c.UserId == userId);
            }
    
            public async Task<IEnumerable<Condition>> AllAsync()
            {
                return await _conditionsRepository.GetAllAsync();
            }
        }
    }
}