using CourseMarket.Web.Models.Discounts;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
