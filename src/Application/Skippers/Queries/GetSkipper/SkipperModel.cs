using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Domain.Entities;
using System.Collections.Generic;

namespace SkipperAgency.Application.Skippers.Queries.GetSkipper
{
    public class SkipperModel : IMapFrom<Skipper>
    {
        public string Id { get; set; }
        public string Oib { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public float Price { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalSummary { get; set; }
        public string UserPhotoUrl { get; set; }
        public CountryModel Country { get; set; }
        public SkipperLicenceModel UserLicence { get; set; }
        public IEnumerable<SkillModel> ListOfSkills { get; set; }
        public IEnumerable<SkipperLanguageModel> ListOfLanguages { get; set; }
    }
}
