using CourseMarket.Web.Models.Catalogs;
using FluentValidation;

namespace CourseMarket.Web.Validators
{
    public class CourseUpdateInputValidator : AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Field cannot be empty!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Field cannot be empty!");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Field cannot be empty!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Field cannot be empty!").ScalePrecision(2, 6).WithMessage("Wrong format!");
        }
    }
}
