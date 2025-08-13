using CourseMarket.Web.Models.FakePayments;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
