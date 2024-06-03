using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using WebShop.Application.Orders.Queries;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.API.Controllers
{
    [Route("api/create-checkout-session")]
    [ApiController]
    public class CheckoutController(IMediator mediator) : ControllerBase
    {

        [HttpPost("{orderId}")]
        public async Task<ActionResult<CreateSessionResponse>> Create(Guid orderId) // Changed return type to ActionResult<string>
        {
            var domain = "http://localhost:4242";

            var order = await mediator.Send(new GetOrderByIdQuery(orderId));
            Console.WriteLine(order);

            if(order == null) {
                throw new NotFoundException(nameof(Order), orderId.ToString());
            }

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var orderItem in order.OrderItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(orderItem.Price * 100), // Stripe requires amount in cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = orderItem.Name,
                        },
                    },
                    Quantity = orderItem.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/checkout/success",
                CancelUrl = "http://localhost:4200/checkout/fail",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            var response = new CreateSessionResponse { SessionId = session.Id };

            return Ok(response);
        }



    }
}
