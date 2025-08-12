using CourseMarket.Shared.Dtos;
using CourseMarket.Web.Models.FakePayments;
using CourseMarket.Web.Models.Orders;
using CourseMarket.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                CVV = checkoutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!responsePayment)  return new OrderCreatedViewModel() { Error = "No payment received", IsSuccessful = false };

            var orderCreateInput = new OrderCreateInput()
            {
                Address = new AddressCreateInput { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode }
            };

            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput
                {
                    ProductId = x.CourseId,
                    Price = x.Price,
                    PictureUrl = "",
                    ProductName = x.CourseName
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if(!response.IsSuccessStatusCode) return new OrderCreatedViewModel() {Error = "Order could not be created!", IsSuccessful=false};

            return await response.Content.ReadFromJsonAsync<OrderCreatedViewModel>();

            }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            throw new System.NotImplementedException();
        }
    }
}