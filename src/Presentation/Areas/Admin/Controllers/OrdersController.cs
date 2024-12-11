//using Application.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Presentation.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    [Route("Admin/[controller]/[action]")]
//    [Authorize]
//    public class OrdersController : Controller
//    {
//        private readonly IOrderService _OrderService;

//        public OrdersController(IOrderService OrderService)
//        {
//            _OrderService = OrderService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var orders = await _OrderService.GetAllAsync();
//            return View(orders);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            var order = await _OrderService.GetByIdAsync(id);
//            if (order == null)
//            {
//                return NotFound();
//            }
//            return View(order);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> PayAsync(int bookId, int quantity)
//        {
//            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
//            await _OrderService.PayAsync(userId, bookId, quantity);
//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int orderId, int orderItemId, int quantity)
//        {
//            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
//            var updatedOrder = await _OrderService.UpdateAsync(orderId, userId, orderItemId, quantity);
//            return Json(updatedOrder);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Delete(int orderItemIdDelete)
//        {
//            await _OrderService.DeleteAsync(orderItemIdDelete);
//            return RedirectToAction("Index");
//        }
//    }
//}