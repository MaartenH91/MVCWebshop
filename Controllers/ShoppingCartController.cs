using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebshop.Data;
using MVCWebshop.Models;

namespace MVCWebshop.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var user = await _context.Users
                .Include(c => c.ShoppingCart)
                .ThenInclude(w => w.CartItems)
                .ThenInclude(s => s.ShopItem)
                .FirstAsync(u => u.Id == userId);
            // push shoppingcart to view index
            return View(user.ShoppingCart);
        }

        // remove one item from shoppingcart
        [HttpGet, ActionName("RemoveFromBasket")]
        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            var shopItem = await _context.ShopItem.FindAsync(id);

            if (shopItem != null)
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var user = await _context.Users
                    .Include(c=>c.ShoppingCart)
                    .ThenInclude(w=>w.CartItems)
                    .ThenInclude(s=>s.ShopItem)
                    .FirstAsync(u=>u.Id == userId);
                // verwijderen van item uit databank
                _context.CartItems.Remove(user.ShoppingCart.CartItems.FirstOrDefault(i=>i.ShopItem.Id == shopItem.Id));
                await _context.SaveChangesAsync();
            }
            // redirect to the public async task Index => index of shoppingcart
            return RedirectToAction(nameof(Index));
        }

        // Empty complete shoppingcart
        [HttpGet, ActionName("ClearShoppingCart")]
        public async Task<ActionResult> ClearShoppingCart()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var user = await _context.Users
                .Include(c => c.ShoppingCart)
                .ThenInclude(w => w.CartItems)
                .FirstAsync(u => u.Id == userId);
            user.ShoppingCart.Clear();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Make order for complete shoppingcart
        [HttpGet, ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var user = await _context.Users
                .Include(o=>o.Orders)
                .Include(c => c.ShoppingCart)
                .ThenInclude(w => w.CartItems)
                .FirstAsync(u => u.Id == userId);
            if (user.ShoppingCart.CartItems.Any())
            {
                user.Orders.Add(
                    new Order()
                    {
                        OrderAddress = user.Street,
                        ShoppingCart = new ShoppingCart(user.ShoppingCart.CartItems, user.ShoppingCart.ValidUntil),
                        StartOrder = DateTime.Now,
                        Remark = $"Number of ordered items: {user.ShoppingCart.CartItems.Sum(r=>r.Amount)}"
                    });
                user.ShoppingCart.Clear();
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Order");
        }



    }
}
