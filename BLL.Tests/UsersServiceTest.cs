using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using Domain.Interfaces;
using Domain.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BLL.Tests
{
    [TestFixture]
    public class UsersServiceTest
    {
        private List<User> usersDb;
        private UsersService usersService;
        private Mock<IUsersRepository> usersRepository;

        [SetUp]
        public void Setup()
        {
            // Arrange
            usersDb = new List<User>();
            usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(
                    entity =>
                    {
                        if (entity.Phone != null &&
                            entity.Phone.Length >= 10 &&
                            entity.Phone.Length <= 15)
                            usersDb.Add(entity);
                        else throw new DBConcurrencyException();
                    });
            usersRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(() => usersDb);
            usersService = new UsersService(usersRepository.Object);
        }
        
        [Test]
        public async Task CreateAsync_UserAdditionSucceed_CreatesUser()
        {
            // Act
            User user = new User {Phone = "1234567890"};
            await usersService.CreateAsync(user);
            var users = await usersService.AllAsync();
            
            // Assert
            Assert.Contains(user, users.ToList());
            usersRepository.Verify(m => m.AddAsync(user), Times.AtLeastOnce());
            usersRepository.Verify(m => m.GetAllAsync(), Times.AtLeastOnce());
        }
        
        [Test]
        public async Task CreateAsync_UserAdditionFailed_ThrowsException()
        {
            // Act
            var user = new User {Phone = "123"};
            var action = new Func<Task>(() => usersService.CreateAsync(user));
            var users = await usersService.AllAsync();
            
            // Assert
            await action.Should().ThrowAsync<DBConcurrencyException>();
            usersRepository.Verify(m => m.AddAsync(user), Times.AtLeastOnce());
            usersRepository.Verify(m => m.GetAllAsync(), Times.AtLeastOnce());
        }
    }
}