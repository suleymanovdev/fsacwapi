using FluentValidation;
using fsacwapi.Core.DTOs.UserDTO.Request;

namespace fsacwapi.Core.Validations;

public class LoginRequestValidation : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}
