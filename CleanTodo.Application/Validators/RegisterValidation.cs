using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.DTOS.Auth;
using FluentValidation;

namespace CleanTodo.Application.Validators;

// Valide automatiquement CreateTodoDto quand il est créé dans le controller
// Validator ci-dessous
public class RegisterValidation : AbstractValidator<RegisterDto>
{
    public RegisterValidation()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est obligatoire.")
            .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
            .MaximumLength(200).WithMessage("Le mot de passe ne peut pas dépasser 200 caractères.")
            .Matches(@"[0-9]").WithMessage("Le mot de passe doit contenir au moins un chiffre.")
            .Matches(@"[\W_]").WithMessage("Le mot de passe doit contenir au moins un caractère spécial ($!@#%^&* etc.).")
            .Matches(@"[A-Z]").WithMessage("Le mot de passe doit contenir au moins une lettre majucule");

    }
}