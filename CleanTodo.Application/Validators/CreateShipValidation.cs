using CleanTodo.Domain.DTOS.Ship;
using CleanTodo.Domain.DTOS.Todo;
using FluentValidation;

namespace CleanTodo.Application.Validators;

// Valide automatiquement CreateTodoDto quand il est créé dans le controller
// Validator ci-dessous
public class CreateShipValidation : AbstractValidator<CreateShipDto>
{
    public CreateShipValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
        RuleFor(x => x.GoldCargo)
            .NotNull()
            .InclusiveBetween(0,1000000);
        RuleFor(x => x.Captain)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
        RuleFor(x => x.CrewSize)
           .NotNull()
           .InclusiveBetween(1, 500);
    }
}