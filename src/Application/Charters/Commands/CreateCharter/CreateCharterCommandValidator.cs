﻿using System.Linq;
using FluentValidation;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Charters.Commands.CreateCharter
{

    public class CreateCharterCommandValidator : AbstractValidator<CreateCharterCommand>
    {
        public CreateCharterCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .Must(email => 
                context.AppUsers.FirstOrDefault(x => x.Email == email) is null && 
                context.SkipperPreRegistration.FirstOrDefault(x => x.Email == email) is null)
                .WithMessage("Email already taken");

            RuleFor(x => x.GdprConsentAccepted).Must(x => x is true);
        }
    }
}
