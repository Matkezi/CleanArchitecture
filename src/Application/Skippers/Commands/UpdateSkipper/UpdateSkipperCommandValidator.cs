using System.IO;
using FluentValidation;
using SkipperAgency.Application.Common.ExtensionMethods;
using SkipperAgency.Application.Common.Interfaces;


namespace SkipperAgency.Application.Skippers.Commands.UpdateSkipper
{
    public class UpdateSkipperCommandValidator : AbstractValidator<UpdateSkipperCommand>
    {
        public UpdateSkipperCommandValidator(ICurrentUserService currentUserService)
        {
            RuleFor(x => x.Id).IsCurrentUserId(currentUserService.UserId);
            RuleFor(x => x.UserPhoto)
                .Must(file => Path.HasExtension(file.NameWithExt))
                .ContainsValidBase64Data()
                .When(file => file != null);
            RuleFor(x => x.UserLicense)
                .Must(file => Path.HasExtension(file.NameWithExt))
                .ContainsValidBase64Data()
                .When(file => file != null);
        }
    }
}