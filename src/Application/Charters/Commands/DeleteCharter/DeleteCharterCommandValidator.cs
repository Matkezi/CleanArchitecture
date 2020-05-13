using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Charters.Commands.DeleteCharter
{
    public class DeleteCharterCommandValidator : AbstractValidator<DeleteCharterCommand>
    {
        public DeleteCharterCommandValidator(ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Id).IsCurrentUserId(currentUserService.UserId);
        }
    }
}
