using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CleanArchitecture.Infrastructure.Persistence.Entities
{
    public class Language
    {
        public int Id { get; set; }

        public string EnglishName { get; set; }
        public string TwoLetterCode { get; set; }    

        public List<SkipperLanguage> Skippers { get; set; }
    }
}
