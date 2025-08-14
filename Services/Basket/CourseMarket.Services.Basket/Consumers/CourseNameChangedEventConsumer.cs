using CourseMarket.Services.Basket.Dtos;
using CourseMarket.Services.Basket.Services;
using CourseMarket.Shared.Messages;
using MassTransit;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseMarket.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;

        public CourseNameChangedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var server = _redisService.GetDb().Multiplexer.GetServer(_redisService.GetDb().Multiplexer.GetEndPoints().First());
            var keys = server.Keys(_redisService.DbIndex).Select(k => k.ToString());
            foreach (var key in keys)
            {
                var value = await _redisService.GetDb().StringGetAsync(key);
                if (!string.IsNullOrEmpty(value))
                {
                    var basketDto = JsonSerializer.Deserialize<BasketDto>(value);
                    if (basketDto != null && basketDto.BasketItems != null)
                    {
                        var basketItems = basketDto.BasketItems.Where(x => x.CourseId == context.Message.CourseId);
                        foreach (var item in basketItems)
                        {
                            item.CourseName = context.Message.UpdatedName;
                        }
                        await _redisService.GetDb().StringSetAsync(key, JsonSerializer.Serialize(basketDto));
                    }
                }
            }


        }
    }
}
