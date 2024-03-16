using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using UserManagement.Models;

namespace UserManagement.Data.Tests
{
    public class DataContextTests
    {
        private DataContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new DataContext();
        }

        [Fact]
        public void CreateUser_WhenCalled_CreatesNewUserRecord()
        {
            // Arrange
            var user = new User
            {
                Forename = "Test",
                Surname = "User",
                Email = "testuser@example.com",
                IsActive = true
            };

            // Act
            _context.CreateUser(user);

            var dbUser = _context.GetUser(user.Id);

            // Assert
            dbUser.Should().NotBeNull();
            dbUser.Should().BeEquivalentTo(user);

            // Check that a log entry was created
            var logEntry = _context.GetUserLogEntries(user.Id).FirstOrDefault();
            logEntry.Should().NotBeNull();
            logEntry.Action.Should().Be(UserLogAction.Create);
        }

        // Other tests

    }

    public class UserManagementDataContextTests
    {
        // Other tests
    }

}

namespace UserManagement.Data
{
    public class DataContext
    {
        // Other methods

        public void CreateUser(User user)
        {
            _users.Add(user.Id, user);

            // Create a log entry for the new user
            CreateLogEntry(user.Id, UserLogAction.Create);
        }

        public void DeleteUser(int userId)
        {
            _users.TryRemove(userId, out var user);

            // Create a log entry for the deleted user
            CreateLogEntry(userId, UserLogAction.Delete);
        }

        public void UpdateUser(User user)
        {
            _users.AddOrUpdate(user.Id, user);

            // Create a log entry for the updated user
            CreateLogEntry(user.Id, UserLogAction.Update);
        }

        public User GetUser(int userId)
        {
            _users.TryGetValue(userId, out var user);

            var userLogs = GetUserLogEntries(userId);

            return user;
        }

        public IEnumerable<UserLogEntry> GetUserLogEntries(int userId)
        {
            return _userLogs.Where(x => x.Key == userId).Select(x => x.Value);
        }

        public void CreateLogEntry(int userId, UserLogAction action)
        {
            _userLogs.TryAdd(userId, new UserLogEntry
            {
                UserId = userId,
                Action = action
            });
        }
    }

    public class UserLogEntry
    {
        public int UserId { get; set; }
        public UserLogAction Action { get; set; }
    }

    public enum UserLogAction
    {
        Create,
        Update,
        Delete
    }

}