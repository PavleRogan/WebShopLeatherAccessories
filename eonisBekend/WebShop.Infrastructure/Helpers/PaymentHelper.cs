using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Application.Payment;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.Infrastructure.Helpers
{
    public class PaymentHelper : IPaymentHelper
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly StripeSettings _stripeSettings;
        public PaymentHelper(IOrdersRepository ordersRepository, IOptions<StripeSettings> stripeSettings) {

            _ordersRepository = ordersRepository;
            _stripeSettings = stripeSettings.Value;

        }
        public async Task<Domain.Entities.Order> CreateOrUpdatePaymentIntent(Guid orderId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var order = await _ordersRepository.GetById(orderId);

            if(order == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), orderId.ToString());
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if(string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)order.OrderItems.Sum(i => i.Quantity * (i.Price * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                intent = await service.CreateAsync(options);
                order.PaymentIntentId = intent.Id;
                order.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)order.OrderItems.Sum(i => i.Quantity * (i.Price * 100))
                };
                await service.UpdateAsync(order.PaymentIntentId, options);
            }

            await _ordersRepository.SaveChanges();
            return order;
        }
    }
}
