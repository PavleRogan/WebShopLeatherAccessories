using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Climate;
using WebShop.Application.Orders.Dtos;
using WebShop.Application.Payment;
using WebShop.Domain.Entities;

namespace WebShop.API.Controllers;


[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly string _whSecret;
    private readonly ILogger<PaymentController> _logger;
    private readonly IPaymentHelper _paymentService;
    private readonly IMapper _mapper;

    public PaymentController(IPaymentHelper paymentService, ILogger<PaymentController> logger,
        IConfiguration config, IMapper mapper)
    {
        _logger = logger;
        _paymentService = paymentService;
        _whSecret = config.GetSection("StripeSettings:WhSecret").Value;
        _mapper = mapper;
    }

    [Authorize]
    [HttpPost("{orderId}")]
    public async Task<ActionResult<OrderDto>> CreateOrUpdatePaymentIntent(Guid orderId)
    {
        var order = await _paymentService.CreateOrUpdatePaymentIntent(orderId);

        if (order == null) return BadRequest();


        return _mapper.Map<OrderDto>(order); ;
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], _whSecret);

        PaymentIntent intent;
        //Order order;

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment succeeded: ", intent.Id);
               // order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
              //  _logger.LogInformation("Order updated to payment received: ", order.Id);
                break;
            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                _logger.LogInformation("Payment failed: ", intent.Id);
               // order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
               // _logger.LogInformation("Order updated to payment failed: ", order.Id);
                break;
        }

        return new EmptyResult();
    }
}

