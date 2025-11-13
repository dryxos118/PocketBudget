using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PocketApi.Models;
using PocketApi.Models.Dto;

namespace PocketApi.Test.Controllers;

public class AuthControllerTests
{
    // private readonly Mock<ILogger<AuthController>> _loggerMock;
    // private readonly Mock<IPocketAuthService> _pocketAuthServiceMock;
    // private readonly AuthController _authController;
    //
    // public AuthControllerTests()
    // {
    //     _loggerMock = new Mock<ILogger<AuthController>>();
    //     _pocketAuthServiceMock = new Mock<IPocketAuthService>();
    //
    //     _authController = new AuthController(_loggerMock.Object, _pocketAuthServiceMock.Object);
    // }
    //
    // [Fact]
    // public async Task Login_ReturnsSuccess()
    // {
    //     //Arrange
    //     LoginDto login = new() { Email = "ab@test.com", Password = "Secret123" };
    //     AuthResponseDto response = new()
    //     {
    //         Token = "JWTToken",
    //         FullName = "AB"
    //     };
    //
    //     _pocketAuthServiceMock
    //         .Setup(s => s.Login(It.Is<LoginDto>(d => d.Email == login.Email && d.Password == login.Password)))
    //         .ReturnsAsync(response);
    //
    //     //Act
    //     IActionResult result = await _authController.Login(login);
    //
    //     //Assert
    //     Assert.NotNull(result);
    //     OkObjectResult ok = Assert.IsType<OkObjectResult>(result);
    //     AuthResponseDto payload = Assert.IsType<AuthResponseDto>(ok.Value);
    //     Assert.Same(response, payload);
    //     _pocketAuthServiceMock.Verify(s => s.Login(It.Is<LoginDto>(d => d.Email == login.Email && d.Password == login.Password)), Times.Once);
    //     _pocketAuthServiceMock.VerifyNoOtherCalls();
    // }
    //
    // [Fact]
    // public async Task Login_ReturnsBadRequest()
    // {
    //     //Arrange
    //     TestUtils.SetupControllerContext(_authController, "Auth", "Login");
    //     LoginDto login = new() { Email = "ab@test.com", Password = "Secret123" };
    //
    //     _pocketAuthServiceMock.Setup(s => s.Login(It.IsAny<LoginDto>())).ReturnsAsync((AuthResponseDto)null!);
    //
    //     //Act
    //     IActionResult result = await _authController.Login(login);
    //
    //     //Assert
    //     Assert.NotNull(result);
    //     TestUtils.AssertPocketController(result, ErrorType.BadRequest);
    //     _pocketAuthServiceMock.Verify(s => s.Login(It.IsAny<LoginDto>()), Times.Once);
    //     _pocketAuthServiceMock.VerifyNoOtherCalls();
    // }
}