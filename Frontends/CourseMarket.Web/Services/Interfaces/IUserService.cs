using CourseMarket.Web.Models;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
