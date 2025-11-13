using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PocketApi.Data;
using PocketApi.Models;
using PocketApi.Models.Config;
using PocketApi.Models.Dto;

namespace PocketApi.Test.Services;

public class PocketAuthServiceTests
{
    // private readonly Mock<IOptions<TokenSettings>> _tokenSettingsMock;
    //
    // public PocketAuthServiceTests()
    // {
    //     _tokenSettingsMock = new Mock<IOptions<TokenSettings>>();
    //     _tokenSettingsMock.Setup(x => x.Value).Returns(new TokenSettings
    //     {
    //         Key = "THIS_IS_MY_TEST_SECRET_KEY_1234567890",
    //         Issuer = "PocketBudget",
    //         Audience = "PocketBudgetUsers"
    //     });
    // }
    //
    // [Fact]
    // public async Task LoginAsync_Success()
    // {
    //     // Arrange
    //     PocketBudgetContext context = TestUtils.GetInMemoryDbContext();
    //     PocketUser user = new()
    //         { UserId = 1, Email = "user1@test.com", Password = BCrypt.Net.BCrypt.HashPassword("password") };
    //     context.Users.Add(user);
    //     await context.SaveChangesAsync();
    //
    //     LoginDto loginDto = new() { Email = "user1@test.com", Password = "password" };
    //     PocketAuthService authService = new PocketAuthService(_tokenSettingsMock.Object, context);
    //
    //     // Act
    //     var result = await authService.Login(loginDto);
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.IsType<AuthResponseDto>(result);
    //     Assert.NotNull(result.FullName);
    //     Assert.NotNull(result.Token);
    //     Assert.NotEmpty(result.FullName);
    //     Assert.NotEmpty(result.Token);
    // }
}