using FluentValidation;

namespace SkipperAgency.Application.Identity.ExternalIdentity.Facebook
{
    public class FacebookLoginCommandValidator : AbstractValidator<FacebookLoginCommand>
    {
        public FacebookLoginCommandValidator()
        {
            RuleFor(x => x.AuthToken).NotEmpty();
        }
    }
}
