using PocketApi.Models.Dto;

namespace PocketApi.Test.Models;

public class LoginDtoValidationTests
{
    [Fact]
    public void ValidModel_ShouldBeValid()
    {
        // Arrange
        LoginDto dto = new LoginDto
        {
            Email = "user@example.com",
            Password = "superpass123"
        };

        // Act
        var results = TestUtils.ValidateModel(dto);

        // Assert
        Assert.Empty(results);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("user@")]
    [InlineData("user@example")]
    public void Email_MustBeValidFormat(string badEmail)
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = badEmail,
            Password = "superpass123"
        };

        // Act
        var results = TestUtils.ValidateModel(dto);

        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginDto.Email)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234")]
    [InlineData("abcd")]
    public void Password_MustBeValidFormat(string badPassword)
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "user@example.com",
            Password = badPassword
        };
        
        // Act
        var results = TestUtils.ValidateModel(dto);
        
        // Assert
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(LoginDto.Password)));
    }
}