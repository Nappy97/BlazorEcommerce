using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    // 카트에 담긴 정보(로컬 스토리지)
    [HttpPost("products")]
    public async Task<ActionResult<ServiceResponse<List<CartProductResponseDto>>>> GetCartProducts(
        List<CartItem> cartItems)
    {
        var result = await _cartService.GetCartProducts(cartItems);
        return Ok(result);
    }

    // 카트에 담긴 정보(회원)
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<CartProductResponseDto>>>> StoreCartItems(
        List<CartItem> cartItems)
    {
        var result = await _cartService.StoreCartItems(cartItems);
        return Ok(result);
    }

    // 카트에 추가
    [HttpPost("add")]
    public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
    {
        var result = await _cartService.AddToCart(cartItem);
        return Ok(result);
    }
    
    // 아이템 수량변경
    [HttpPut("update-quantity")]
    public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartItem cartItem)
    {
        var result = await _cartService.UpdateQuantity(cartItem);
        return Ok(result);
    }
    
    // 카트에서 상품제거
    [HttpDelete("{productId:int}/{productTypeId:int}")]
    public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var result = await _cartService.RemoveItemFromCart(productId, productTypeId);
        return Ok(result);
    }

    // 카트에 담긴 숫자
    [HttpGet("count")]
    public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
    {
        return await _cartService.GetCartItemCount();
    }

    // DB에서 카트 정보 얻어오기
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<CartProductResponseDto>>>> GetDbCartProducts()
    {
        var result = await _cartService.GetDbCartProducts();
        return Ok(result);
    }
}