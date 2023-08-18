using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly ICartService _cartService;
    private readonly IAuthService _authService;
    private readonly IOrderService _orderService;

    private const string secret = "whsec_a0ed78aec3bb63e97991a0fff45ffa798fd418f7a34ce97b0188e4aaa5f8f80f";

    public PaymentService(ICartService cartService, IAuthService authService,
        IOrderService orderService)
    {
        StripeConfiguration.ApiKey =
            "sk_test_51NgJuoLlyf9pTszV0dNeRQVTEpJuWKE7ZYkIjot04ZvjVPGAbyl0MsPtLcFMQtyqtpaLF9tcOxx4kj9uIiQgciwy00Pn8XykuI";
        _cartService = cartService;
        _authService = authService;
        _orderService = orderService;
    }

    public async Task<Session> CreateCheckoutSession()
    {
        var products = (await _cartService.GetDbCartProducts()).Data;
        var lineItems = new List<SessionLineItemOptions>();
        products.ForEach(product =>
            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl }
                    }
                },
                Quantity = product.Quantity
            }));

        var options = new SessionCreateOptions
        {
            CustomerEmail = _authService.GetUserEmail(),
            ShippingAddressCollection =
                new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "US", "KR" }
                },
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "http://localhost:5216/order-success", // watch run 용도
            CancelUrl = "http://localhost:5216/cart"
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return session;
    }

    // 웹훅
    public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                request.Headers["Stripe-Signature"],
                secret
            );

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var user = await _authService.GetUserByEmail(session.CustomerEmail);
                await _orderService.PlaceOrder(user.Id);
            }

            return new ServiceResponse<bool> { Data = true };
        }
        catch (StripeException e)
        {
            return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
        }
    }
}