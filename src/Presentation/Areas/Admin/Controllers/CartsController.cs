using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var cart = await _cartService.GetByUserIdAsync(userId);
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, int quantity)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError(string.Empty, "Quantity must be greater than zero.");
                return RedirectToAction(nameof(Index), "Books", new { area = "Admin" }); 
            }

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            try
            {
                await _cartService.AddToCartAsync(userId, bookId, quantity);
                return RedirectToAction(nameof(Index), "Books", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index), "Books", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int cartId, int cartItemId, int quantity)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError(string.Empty, "Quantity must be greater than zero.");
                return RedirectToAction(nameof(Index), "Books", new { area = "Admin" });
            }
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedCart = await _cartService.UpdateAsync(cartId, userId, cartItemId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int cartItemIdDelete)
        {
            await _cartService.DeleteAsync(cartItemIdDelete);
            return RedirectToAction(nameof(Index));
        }
    }
}
