using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CleanArchitecture.Application.ExternalLogins.Facebook
{
    public class FacebookLoginCommandValidator : AbstractValidator<FacebookLoginCommand>
    {
        public FacebookLoginCommandValidator()
        {
            RuleFor(x => x.AuthToken).NotEmpty();
        }
    }
}
