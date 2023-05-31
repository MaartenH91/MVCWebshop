using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebshop.Data;
using MVCWebshop.Models;

namespace MVCWebshop.Controllers
{
    public class ShopItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShopItem
        public async Task<IActionResult> Index()
        {
              return _context.ShopItem != null ? 
                          View(await _context.ShopItem.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ShopItem'  is null.");
        }

        // GET: ShopItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShopItem == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopItem == null)
            {
                return NotFound();
            }

            return View(shopItem);
        }

        // GET: ShopItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShopItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kind,Name,Price,Description")] ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        // GET: ShopItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShopItem == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem.FindAsync(id);
            if (shopItem == null)
            {
                return NotFound();
            }
            return View(shopItem);
        }

        // POST: ShopItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kind,Name,Price,Description")] ShopItem shopItem)
        {
            if (id != shopItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopItemExists(shopItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        // GET: ShopItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShopItem == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopItem == null)
            {
                return NotFound();
            }

            return View(shopItem);
        }

        // POST: ShopItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShopItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ShopItem'  is null.");
            }
            var shopItem = await _context.ShopItem.FindAsync(id);
            if (shopItem != null)
            {
                _context.ShopItem.Remove(shopItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id, int amount)
        {
            // check if amount is not less then 1 before adding
            if (amount <= 0)
            {
                return Problem("Minimum amount is 1");
            }
            // check if cart exists
            if(_context.CartItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CartItems' is null.");
            }
            // checken if shopitem exists in databank
            var shopItem = await _context.ShopItem.FindAsync(id);
            if (shopItem != null)
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                // check all users in context, ask for user met same id as user who called addtobasket.
                // include items into shoppingcart
                var user = await _context.Users.Where(u => u.Id == userId).Include(c => c.ShoppingCart).ThenInclude(w => w.CartItems).ThenInclude(c => c.ShopItem).FirstAsync();
                user.ShoppingCart.AddItem(shopItem, amount);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopItemExists(int id)
        {
          return (_context.ShopItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
