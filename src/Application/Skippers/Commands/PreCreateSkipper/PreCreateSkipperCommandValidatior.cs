using FluentValidation;

namespace CleanArchitecture.Application.Skippers.Commands.PreCreateSkipper
{

    public class PreCreateSkipperCommandValidatior : AbstractValidator<PreCreateSkipperCommand>
    {
        public PreCreateSkipperCommandValidatior()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
            //.Must(userService.IsEmailUnique)
            //.WithMessage("Email already taken");

        }
    }
}
