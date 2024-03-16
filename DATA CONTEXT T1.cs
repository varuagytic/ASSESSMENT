using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using UserManagement.Models;

namespace UserManagement.Data.Tests
{
    public class DataContextTests
    {
        [Fact]
        public void GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
        {
            // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
            var context = CreateContext();

            var entity = new User
            {
                Forename = "Brand New",
                Surname = "User",
                Email = "brandnewuser@example.com",
                IsActive = true
            };
            context.Create(entity);

            // Act: Invokes the method under test with the arranged parameters.
            var result = context.GetAll<User>();

            // Assert: Verifies that the action of the method under test behaves as expected.
            result
                .Should().Contain(s => s.Email == entity.Email)
                .Which.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void GetAll_WhenDeleted_MustNotIncludeDeletedEntity()
        {
            // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
            var context = CreateContext();
            var entity = context.GetAll<User>().First();
            context.Delete(entity);

            // Act: Invokes the method under test with the arranged parameters.
            var result = context.GetAll<User>();

            // Assert: Verifies that the action of the method under test behaves as expected.
            result.Should().NotContain(s => s.Email == entity.Email);
        }

        [Fact]
        public void GetActiveUsers_WhenCalled_MustReturnActiveUsers()
        {
            // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
            var context = CreateContext();

            var activeUser = new User
            {
                Forename = "Active",
                Surname = "User",
                Email = "activeuser@example.com",
                IsActive = true
            };
            context.Create(activeUser);

            var inactiveUser = new User
            {
                Forename = "Inactive",
                Surname = "User",
                Email = "inactiveuser@example.com",
                IsActive = false
            };
            context.Create(inactiveUser);

            // Act: Invokes the method under test with the arranged parameters.
            var result = context.GetActiveUsers();

            // Assert: Verifies that the action of the method under test behaves as expected.
            result.Should().ContainEquivalentOf(activeUser);
            result.Should().NotContainEquivalentOf(inactiveUser);
        }

        [Fact]
        public void GetInactiveUsers_WhenCalled_MustReturnInactiveUsers()
        {
            // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
            var context = CreateContext();

            var activeUser = new User
            {
                Forename = "Active",
                Surname = "User",
                Email = "activeuser@example.com",
                IsActive = true
            };
            context.Create(activeUser);

            var inactiveUser = new User
            {
                Forename = "Inactive",
                Surname = "User",
                Email = "inactiveuser@example.com",
                IsActive = false
            };
            context.Create(inactiveUser);

            // Act: Invokes the method under test with the arranged parameters.
            var result = context.GetInactiveUsers();

            // Assert: Verifies that the action of the method under test behaves as expected.
            result.Should().NotContainEquivalentOf(activeUser);
            result.Should().ContainEquivalentOf(inactiveUser);
        }

        private DataContext CreateContext()