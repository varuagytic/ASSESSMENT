[Fact]
public void GetAll_WhenNewEntityAdded_MustIncludeNewEntity()
{
    // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
    var context = CreateContext();

    var now = System.DateTime.UtcNow;

    var entity = new User
    {
        Forename = "Brand New",
        Surname = "User",
        Email = "brandnewuser@example.com",
        IsActive = true,
        DateOfBirth = now
    };
    context.Create(entity);

    // Act: Invokes the method under test with the arranged parameters.
    var result = context.GetAll<User>();

    // Assert: Verifies that the action of the method under test behaves as expected.
    var user = result.Single(u => u.Email == entity.Email);

    user.Should().BeEquivalentTo(entity);

    user.DateOfBirth.Should().Be(entity.DateOfBirth);
}