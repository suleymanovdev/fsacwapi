using FluentValidation;
using fsacwapi.Core.DTOs.UserDTO.Request;

namespace fsacwapi.Core.Validations;

public class RegisterRequestValidation : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}