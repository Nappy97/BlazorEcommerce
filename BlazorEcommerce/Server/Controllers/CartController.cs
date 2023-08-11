using System.Security.Claims;
using BlazorEcommerce.Shared.Dto;
using BlazorEcommerce.Shared.Model;
using BlazorEcommerce.Shared.Model.Internal;
using BlazorEcommerce.Shared.Response;
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
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _cartService.StoreCartItems(cartItems, userId);
        return Ok(result);
    }
}