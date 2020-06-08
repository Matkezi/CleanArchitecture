using AutoMapper;
using SkipperAgency.Application.Bookings.CommonModels;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetCountries;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Application.Skippers.Common.Models;
using SkipperAgency.Application.Skippers.Queries.Availability.Common.Models;
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
        public int YearOfFirstLicense { get; set; }
        public string UserPhotoUrl { get; set; }
        public IEnumerable<BookingDateRangeModel> Booked { get; set; }
        public IEnumerable<AvailabilityDateRangeModel> Available { get; set; }
        public CountryModel Country { get; set; }
        public IEnumerable<BookingModel> Bookings { get; set; }
        public SkipperLicenseModel UserLicense { get; set; }
        public List<SkillModel> ListOfSkills { get; set; }
        public IEnumerable<SkipperLanguageModel> ListOfLanguages { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Skipper, SkipperModel>()
                   .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.Availability));
        }
    }
}
