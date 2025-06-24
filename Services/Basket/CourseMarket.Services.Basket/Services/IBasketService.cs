using CourseMarket.Services.Basket.Dtos;
using CourseMarket.Shared.Dtos;
using System.Threading.Tasks;

namespace CourseMarket.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
