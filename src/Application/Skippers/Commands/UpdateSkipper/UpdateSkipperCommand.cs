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
    public class UpdateSkipperCommand : IRequest, ISkipperAuth
    {
        public string SkipperId { get; set; }
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
        public SkipperLicenceModel UserLicence { get; set; }


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
                var entity = await _context.Skipper.Include(x => x.ListOfLanguages).Include(x => x.ListOfSkills).FirstOrDefaultAsync(x => x.Id == request.SkipperId);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Skipper), request.SkipperId);
                }

                entity.OIB = request.Oib;
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.Address = request.Address;
                entity.ZipCode = request.ZipCode;
                entity.City = request.City;
                entity.PhoneNumber = request.PhoneNumber;
                entity.Price = request.Price;
                entity.PersonalSummary = request.PersonalSummary;
                entity.CountryId = request.CountryId;

                _context.TryUpdateManyToMany(entity.ListOfLanguages,
                    request.ListOfLanguages.Select(language => new SkipperLanguage
                    {
                        LanguageId = language.LanguageId,
                        LevelOfKnowledge = language.LevelOfKnowledge,
                        SkipperId = language.SkipperId
                    }), x => x.LanguageId);


                _context.TryUpdateManyToMany(entity.ListOfSkills,
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
                        entity.UserPhotoUrl);

                    entity.UserPhotoUrl = photoUri;
                }
                if (request.UserLicence != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var licenceUri = await _filesStorageService.ReplaceCloudAsync(
                        request.UserPhoto.Data,
                        Path.GetExtension(request.UserPhoto.Name),
                        entity.Licence.LicenceUrl
                        );

                    entity.Licence = new Licence
                    {
                        DateOfIssue = request.UserLicence.DateOfIssue,
                        ValidTo = request.UserLicence.ValidTo,
                        LicenceType = request.UserLicence.LicenceType,
                        LicenceUrl = licenceUri
                    };
                }

                await _context.SaveChangesAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.NewEmail))
                {
                    _ = _mediator.Send(new EmailChangeRequestCommand { UserEmail = entity.Email, UserNewEmail = request.NewEmail });
                }

                return Unit.Value;
            }
        }
    }
}
