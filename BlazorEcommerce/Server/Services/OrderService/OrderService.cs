namespace BlazorEcommerce.Server.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly DataContext _context;
    private readonly ICartService _cartService;
    private readonly IAuthService _authService;

    public OrderService(DataContext context, ICartService cartService,
        IAuthService authService)
    {
        _context = context;
        _cartService = cartService;
        _authService = authService;
    }

    // 장바구니에서 주문을 생성합니다.
    public async Task<ServiceResponse<bool>> PlaceOrder()
    {
        var products = (await _cartService.GetDbCartProducts()).Data;
        decimal totalPrice = 0;
        products.ForEach(product => totalPrice += product.Price * product.Quantity);

        var orderItems = new List<OrderItem>();
        products.ForEach(product => orderItems.Add(new OrderItem
        {
            ProductId = product.ProductId,
            ProductTypeId = product.ProductTypeId,
            Quantity = product.Quantity,
            TotalPrice = product.Price * product.Quantity
        }));

        var order = new Order
        {
            UserId = _authService.GetUserId(),
            OrderDate = DateTime.Now,
            OrderItems = orderItems,
            TotalPrice = totalPrice
        };

        _context.Orders.Add(order);

        _context.CartItems.RemoveRange(_context.CartItems
            .Where(ci => ci.UserId == _authService.GetUserId()));

        await _context.SaveChangesAsync();

        return new ServiceResponse<bool> { Data = true, Message = "Order placed successfully!" };
    }

    // 주문목록을 가져옵니다.
    public async Task<ServiceResponse<List<OrderOverviewResponseDto>>> GetOrders()
    {
        var response = new ServiceResponse<List<OrderOverviewResponseDto>>();
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == _authService.GetUserId())
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        var orderResponse = new List<OrderOverviewResponseDto>();
        orders.ForEach(o => orderResponse.Add(new OrderOverviewResponseDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1
                    ? $"{o.OrderItems.First().Product.Title} and" +
                      $" {o.OrderItems.Count - 1} more..."
                    : o.OrderItems.First().Product.Title,
                ProductImageUrl = o.OrderItems.First().Product.ImageUrl
            })
        );

        response.Data = orderResponse;

        return response;
    }
}