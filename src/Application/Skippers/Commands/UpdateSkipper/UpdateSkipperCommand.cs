using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.Common.Helpers;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Identity.Commands.EmailChangeRequest;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Skippers.Commands.UpdateSkipper
{
    public class UpdateSkipperCommand : IRequest, IUserAuth
    {
        public string Id { get; set; }
        public string Oib { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public float Price { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string PersonalSummary { get; set; }
        public IEnumerable<SkillModel> ListOfSkills { get; set; }
        public IEnumerable<SkipperLanguageModel> ListOfLanguages { get; set; }

        public string NewEmail { get; set; }
        public FileModel UserPhoto { get; set; }
        public SkipperLicenseModel UserLicense { get; set; }


        public class Handler : IRequestHandler<UpdateSkipperCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            private readonly IIdentityService _identityService;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService, IIdentityService identityService, IMediator mediator)
            {
                _context = context;
                _filesStorageService = filesStorageService;
                _identityService = identityService;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(UpdateSkipperCommand request, CancellationToken cancellationToken)
            {
                var skipper = await _context.Skipper.Include(x => x.ListOfLanguages).Include(x => x.ListOfSkills).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (skipper is null)
                {
                    throw new NotFoundException(nameof(Skipper), request.Id);
                }

                skipper.Oib = request.Oib;
                skipper.FirstName = request.FirstName;
                skipper.LastName = request.LastName;
                skipper.Address = request.Address;
                skipper.ZipCode = request.ZipCode;
                skipper.City = request.City;
                skipper.PhoneNumber = request.PhoneNumber;
                skipper.Price = request.Price;
                skipper.PersonalSummary = request.PersonalSummary;
                skipper.CountryId = request.CountryId;

                _context.TryUpdateManyToMany(skipper.ListOfLanguages,
                    request.ListOfLanguages.Select(language => new SkipperLanguage
                    {
                        LanguageId = language.LanguageId,
                        LevelOfKnowledge = language.LevelOfKnowledge,
                        SkipperId = language.SkipperId
                    }), x => x.LanguageId);


                _context.TryUpdateManyToMany(skipper.ListOfSkills,
                    request.ListOfSkills.Select(skill => new SkipperSkill
                    {
                        SkillId = skill.SkillId,
                        SkipperId = skill.SkipperId
                    }), x => x.SkillId);


                //entity.ListOfLanguages.Clear();
                //foreach (var language in request.ListOfLanguages)
                //{
                //    entity.ListOfLanguages.Add(new SkipperLanguage
                //    {
                //        LanguageId = language.LanguageId,
                //        LevelOfKnowledge = language.LevelOfKnowledge,
                //        SkipperId = language.SkipperId
                //    });
                //}

                //entity.ListOfLanguages.ForEach(language => _context.SkipperLanguages.Remove(language));
                //entity.ListOfLanguages = request.ListOfLanguages.ToList()
                //    .ConvertAll(language => new SkipperLanguage
                //    {
                //        LanguageId = language.LanguageId,
                //        LevelOfKnowledge = language.LevelOfKnowledge,
                //        SkipperId = language.SkipperId
                //    });

                if (request.UserPhoto != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var photoUri = await _filesStorageService.ReplaceCloudAsync(
                        request.UserPhoto.Data,
                        Path.GetExtension(request.UserPhoto.Name),
                        skipper.UserPhotoUrl);

                    skipper.UserPhotoUrl = photoUri;
                }
                if (request.UserLicense != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var licenseUri = await _filesStorageService.ReplaceCloudAsync(
                        request.UserPhoto.Data,
                        Path.GetExtension(request.UserPhoto.Name),
                        skipper.License.LicenseUrl
                        );

                    skipper.License = new License
                    {
                        DateOfIssue = request.UserLicense.DateOfIssue,
                        ValidTo = request.UserLicense.ValidTo,
                        LicenseType = request.UserLicense.LicenseType,
                        LicenseUrl = licenseUri
                    };
                }

                await _context.SaveChangesAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.NewEmail))
                {
                    _ = _mediator.Send(new EmailChangeRequestCommand { UserEmail = skipper.Email, UserNewEmail = request.NewEmail });
                }

                return Unit.Value;
            }
        }
    }
}
