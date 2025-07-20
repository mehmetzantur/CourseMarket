using System.ComponentModel.DataAnnotations;

namespace CourseMarket.Web.Models.Catalogs
{
    public class CourseCreateInput
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public string Picture { get; set; }

        public FeatureViewModel Feature { get; set; }
    }
}
