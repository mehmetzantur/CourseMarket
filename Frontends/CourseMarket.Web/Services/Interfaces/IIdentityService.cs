using CourseMarket.Shared.Dtos;
using CourseMarket.Web.Models;
using Duende.IdentityModel.Client;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
