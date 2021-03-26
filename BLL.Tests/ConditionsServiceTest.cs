using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Services.BLL.Services;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class ConditionsServiceTest
    {
        private List<User> usersDb;
        private List<Condition> conditionsDb;
        private ConditionsService conditionsService;
        private Mock<IConditionsRepository> conditionsRepository;
        private Mock<IUsersRepository> usersRepository;
        private User user = null;
        
        [SetUp]
        public void Setup()
        {
            // Arrange
            usersDb = new List<User> { new User {Id = 1} };
            conditionsDb = new List<Condition>();
            
            conditionsRepository = new Mock<IConditionsRepository>();
            conditionsRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Condition>()))
                .Callback<Condition>(
                    entity =>
                    {
                        conditionsDb.Add(entity);
                    });
            conditionsRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(() => conditionsDb);
            
            usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(repo => repo.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .Callback((Expression<Func<User, bool>> expression) =>
                {
                    Func<User, bool> expr = expression.Compile();
                    user = usersDb.Where(expr).ToList().FirstOrDefault();
                })
                .ReturnsAsync(() => user);
            
            conditionsService = new ConditionsService(conditionsRepository.Object, usersRepository.Object);
        }
        
        [Test]
        public async Task CreateAsync_ConditionAdditionSucceed_AddsCondition()
        {
            // Act
            Condition condition = new Condition {UserId = 1};
            await conditionsService.CreateAsync(condition);
            var conditions = await conditionsService.AllAsync();
            
            // Assert
            conditionsRepository.Verify(m => m.AddAsync(condition), Times.AtLeastOnce());
            conditionsRepository.Verify(m => m.GetAllAsync(), Times.AtLeastOnce());
            usersRepository.Verify(m => m.FindOneAsync(u => u.Id == condition.UserId), Times.AtLeastOnce());
            Assert.Contains(condition, conditions.ToList());
        }
        
        [Test]
        public async Task CreateAsync_ConditionAdditionFailed_ThrowsException()
        {
            // Act
            Condition condition = new Condition {UserId = 2};
            var action = new Func<Task>(() => conditionsService.CreateAsync(condition));
            var conditions = await conditionsService.AllAsync();
            
            // Assert
            conditionsRepository.Verify(m => m.AddAsync(condition), Times.Never());
            conditionsRepository.Verify(m => m.GetAllAsync(), Times.AtLeastOnce());
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("Not found");
            usersRepository.Verify(m => m.FindOneAsync(u => u.Id == condition.UserId), Times.AtLeastOnce());
        }
    }
}