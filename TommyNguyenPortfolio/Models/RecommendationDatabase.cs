using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TommyNguyenPortfolio.Models
{
    public class RecommendationDatabase
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        public string Recommender { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [Display(Name ="Company You Worked For")]
        public string CompanyWorkedFor { get; set; }

        [Required]
        [Display(Name ="Relationship To Student")]
        public string RelationToStudent { get; set; }

        [Required]
        public string Recommendation { get; set; }

        [Display(Name ="Today's Date")]
        [DataType(DataType.Date)]
        public DateTime DateRecommended { get; set; }


        public PasswordTable passwordTableID { get; set; }
    }
}
