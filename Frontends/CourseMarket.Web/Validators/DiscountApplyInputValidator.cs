using CourseMarket.Web.Models.Discounts;
using FluentValidation;

namespace CourseMarket.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Field cannot be empty!");
        }
    }
}
