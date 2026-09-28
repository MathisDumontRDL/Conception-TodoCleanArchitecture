using CleanTodo.Application.UseCase;
using CleanTodo.Application.Validators;
using CleanTodo.Domain.DTOS.Auth;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace TodoApplicationTests;

public class UsersTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private RegisterUseCase _registerUseCase;
    private IValidator<RegisterDto> _registerValidator;
    User user1 = new User("leUsername", "Bonjour123!");
    User user2 = new User("leDeuxiemeUsername", "PasBonjour123!");


    [SetUp]
    public void Setup()
    {
        _registerValidator = new RegisterValidation();
        _userRepositoryMock = new Mock<IUserRepository>();
        _registerUseCase = new RegisterUseCase(_userRepositoryMock.Object, _registerValidator);

        // Arrange
        _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync(user1);
        _userRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { user1, user2 });
        _userRepositoryMock.Setup(repo => repo.FindById(It.Is<Guid>(id => id == user1.Id))).ReturnsAsync(user2);
    }

    [Test]
    public async Task Register_ShouldReturnCreatedUser()
    {
        // Arrange
        RegisterDto registerDto = new RegisterDto { Username = "Test User", Password="testPassword123!" };

        // Act
        var result = await _registerUseCase.Execute(registerDto);

        // Assert
        Assert.That(user1.Id == result.Id, "User is returned");
        Assert.That(user1.Username == result.Username, "Same username");
        Assert.That(user1.Password == result.Password, "Same password");
    }


    [Test]
    public async Task Register_ShouldReturnExecption()
    {
        Assert.ThrowsAsync<ValidationException>(async () => await _registerUseCase.Execute(new RegisterDto { Username = "unUsernametest", Password="invalide" }));
    }
    //Claude

    // ===== Mots de passe VALIDES : aucune exception =====

    [TestCase("Bonjour123!")]   // cas normal
    [TestCase("Abcdef1!")]      // exactement 8 caractères (limite basse)
    [TestCase("Bonjour_123")]   // le _ compte comme caractère spécial
    [TestCase("Bonjour 123")]   // l'espace compte aussi (\W)
    public void Register_ValidPassword_ShouldNotThrow(string password)
    {
        Assert.DoesNotThrowAsync(async () =>
            await _registerUseCase.Execute(new RegisterDto { Username = "unUsernametest", Password = password }));
    }

    [Test]
    public void Register_Password200Characters_ShouldNotThrow()
    {
        var password = new string('A', 198) + "1!"; // 200 caractères (limite haute)

        Assert.DoesNotThrowAsync(async () =>
            await _registerUseCase.Execute(new RegisterDto { Username = "unUsernametest", Password = password }));
    }

    // ===== Mots de passe INVALIDES : ValidationException =====

    [TestCase(null, "Le mot de passe est obligatoire.")]
    [TestCase("", "Le mot de passe est obligatoire.")]
    [TestCase("Abcde1!", "Le mot de passe doit contenir au moins 8 caractères.")]  // 7 caractères
    [TestCase("Bonjour!!", "Le mot de passe doit contenir au moins un chiffre.")]
    [TestCase("Bonjour123", "Le mot de passe doit contenir au moins un caractère spécial ($!@#%^&* etc.).")]
    [TestCase("bonjour123!", "Le mot de passe doit contenir au moins une lettre majucule")]
    public void Register_InvalidPassword_ShouldThrowValidationException(string? password, string expectedMessage)
    {
        var ex = Assert.ThrowsAsync<ValidationException>(async () =>
            await _registerUseCase.Execute(new RegisterDto { Username = "unUsernametest", Password = password! }));

        Assert.That(ex!.Errors.Select(e => e.ErrorMessage), Does.Contain(expectedMessage));
    }

    [Test]
    public void Register_Password201Characters_ShouldThrowValidationException()
    {
        var password = new string('A', 199) + "1!"; // 201 caractères

        var ex = Assert.ThrowsAsync<ValidationException>(async () =>
            await _registerUseCase.Execute(new RegisterDto { Username = "unUsernametest", Password = password }));

        Assert.That(ex!.Errors.Select(e => e.ErrorMessage),
            Does.Contain("Le mot de passe ne peut pas dépasser 200 caractères."));
    }


}