using FluentValidation;
using SkipperAgency.Application.Common.ExtensionMethods;
using SkipperAgency.Application.Common.Interfaces;


namespace SkipperAgency.Application.Skippers.Commands.UpdateSkipper
{
    public class UpdateSkipperCommandValidator : AbstractValidator<UpdateSkipperCommand>
    {
        public UpdateSkipperCommandValidator(ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Id).IsCurrentUser(currentUserService.UserId);
        }
    }
}