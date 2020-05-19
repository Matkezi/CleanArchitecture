using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Application.Metadata.Queries.GetLanguages;
using SkipperAgency.Application.Skills.Queries.GetSkill;
using SkipperAgency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkipperAgency.Application.Skippers.Common.Models
{
    public class SkipperSimpleModel: IMapFrom<Skipper>
    {
        public string Id { get; set; }
        public string Oib { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PersonalSummary { get; set; }
        public string UserPhotoUrl { get; set; }
        public IEnumerable<SkillModel> ListofSkills { get; set; }
        public IEnumerable<SkipperLanguageModel> ListOfLanguages { get; set; }

    }
}
