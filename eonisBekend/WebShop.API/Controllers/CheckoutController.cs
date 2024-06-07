using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using WebShop.Application.Orders.Commands.UpdateCommand;
using WebShop.Application.Orders.Queries;
using WebShop.Application.Payment;
using WebShop.Domain.Entities;
using WebShop.Domain.Exceptions;
using WebShop.Domain.Repositories;

namespace WebShop.API.Controllers
{
    [Route("api/create-checkout-session")]
    [ApiController]
    public class CheckoutController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly string _whSecret;
        private readonly IMediator mediator;


        public CheckoutController( 
            IConfiguration config, IMediator mediator)
        {
            this.mediator = mediator;
            _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<CreateSessionResponse>> Create(Guid orderId) 
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
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "OrderId", orderId.ToString() }
                    }
                }
            };

            var service = new SessionService();
            Session session = service.Create(options);

            var response = new CreateSessionResponse { SessionId = session.Id };

            return Ok(response);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
             string endpointSecret = _whSecret;
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);

                    var orderId = paymentIntent.Metadata["OrderId"]; // Assuming OrderId is stored in metadata
                    if (Guid.TryParse(orderId, out Guid parsedOrderId))
                    {
                        var updateOrderCommand = new UpdateOrderCommand
                        {
                            OrderId = parsedOrderId,
                            Processed = true
                        };

                        await mediator.Send(updateOrderCommand);
                    }


                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("PAYMENT INTENT WIThhhhH ID: {0} CANCELED.");

                }
                else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PAYMENT INTENT WITH ID: {0} failed.", paymentIntent.Id);
                    // Handle the payment_intent.payment_failed event here
                }
                else if (stripeEvent.Type == Events.PaymentIntentCanceled)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PAYMENT INTENT WITH ID: {0} CANCELED.", paymentIntent.Id);
                    // Handle the payment_intent.payment_failed event here
                }
                else
                {
                    Console.WriteLine("UNHANDELED!! event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}
