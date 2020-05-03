using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Skippers.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkipperBooking.Base.Enums;
using SkipperBooking.Web.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Skippers.Commands.UpdateSkipper
{
    public class UpdateSkipperCommand : IRequest
    {

        public string Id { get; set; }
        public string Oib { get; set; }
        public string NewEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public float Price { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string PersonalSummary { get; set; }
        public FileModel UserPhoto { get; set; }
        public LicenceModel UserLicence { get; set; }        
        public IEnumerable<SkillModel> ListOfSkills { get; set; }
        public IEnumerable<LanguageModel> ListOfLanguages { get; set; }

        public class Handler : IRequestHandler<UpdateSkipperCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IFilesStorageService _filesStorageService;
            private readonly IIdentityService _identityService;

            public Handler(IApplicationDbContext context, IFilesStorageService filesStorageService, IIdentityService identityService)
            {
                _context = context;
                _filesStorageService = filesStorageService;
                _identityService = identityService;
            }

            public async Task<Unit> Handle(UpdateSkipperCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Skipper.Include(x => x.ListOfLanguages).Include(x => x.ListOfSkills).FirstOrDefaultAsync(x => x.Id == request.Id);

                if (entity is null)
                {
                    throw new NotFoundException(nameof(Skipper), request.Id);
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
                        SkillId = (SkillsEnum)skill.Id,
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


                if (!string.IsNullOrEmpty(request.NewEmail))
                {
                    var result = await _identityService.ChangeEmailRequest(entity.UserName, request.NewEmail);
                }
                if (request.UserPhoto != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var photoUri = await _filesStorageService.SaveCloudAsync(request.UserPhoto.Data, Path.GetExtension(request.UserPhoto.Name));

                    if (entity.UserPhotoUrl != null)
                    {
                        await _filesStorageService.DeleteCloudAsync(entity.UserPhotoUrl);
                    }
                    entity.UserPhotoUrl = photoUri;
                }
                if (request.UserLicence != null)
                {
                    // TODO: validate Data somehow before this, make a validator
                    var licenceUri = await _filesStorageService.SaveCloudAsync(request.UserPhoto.Data, Path.GetExtension(request.UserPhoto.Name));

                    if (entity.Licence.LicenceUrl != null)
                    {
                        await _filesStorageService.DeleteCloudAsync(entity.UserPhotoUrl);
                    }
                    entity.Licence = new Licence
                    {
                        DateOfIssue = request.UserLicence.DateOfIssue,
                        ValidTo = request.UserLicence.ValidTo,
                        LicenceType = request.UserLicence.LicenceType,
                        LicenceUrl = licenceUri
                    };
                }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
