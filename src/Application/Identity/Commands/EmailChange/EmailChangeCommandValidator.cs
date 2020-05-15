using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.ExtensionMethods;

namespace SkipperAgency.Application.Identity.Commands.EmailChange
{
    public class EmailChangeCommandValidator : AbstractValidator<EmailChangeCommand>
    {
        public EmailChangeCommandValidator(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            RuleFor(x => x.Email)
                .IsCurrentUserEmail(identityService.GetEmail(currentUserService.UserId));
        }
    }
}