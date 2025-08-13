using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CourseMarket.Web.Models.Catalogs
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public string Picture { get; set; }

        public FeatureViewModel Feature { get; set; }

        [Display(Name = "Course Image")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
