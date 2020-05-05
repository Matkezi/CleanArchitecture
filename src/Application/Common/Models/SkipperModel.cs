using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using SkipperBooking.Web.Models;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Skippers.Models
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
        public LicenceModel UserLicence { get; set; }        
        public IEnumerable<SkillModel> ListOfSkills { get; set; }
        public IEnumerable<LanguageModel> ListOfLanguages { get; set; }
    }
}
