using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Charters.Commands.UpdateCharter
{
    public class UpdateCharterCommandValidator : AbstractValidator<UpdateCharterCommand>
    {
        public UpdateCharterCommandValidator(ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Id).IsCurrentUser(currentUserService.UserId);
        }
    }
}