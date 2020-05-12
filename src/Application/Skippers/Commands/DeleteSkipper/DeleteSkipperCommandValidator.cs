using FluentValidation;
using SkipperAgency.Application.Common.ExtensionMethods;
using SkipperAgency.Application.Common.Interfaces;


namespace SkipperAgency.Application.Skippers.Commands.DeleteSkipper
{
    public class DeleteSkipperCommandValidator : AbstractValidator<DeleteSkipperCommand>
    {
        public DeleteSkipperCommandValidator(ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Id).IsCurrentUser(currentUserService.UserId);
        }
    }
}
