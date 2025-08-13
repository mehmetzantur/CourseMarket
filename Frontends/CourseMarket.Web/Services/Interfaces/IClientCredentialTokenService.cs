using System.Threading.Tasks;

namespace CourseMarket.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetToken();
    }
}
