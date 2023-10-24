using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Data;
using YourPizzaShop.Models;

namespace YourPizzaShop.Pages
{

    [Authorize] 
    public class OrdersModel : PageModel
    {

        public List<PizzaOrder> Orders= new List<PizzaOrder>();

        private readonly YourPizzaShopContext _context; 

       // public OrdersModel(YourPizzaShopContext context)
        //{
          //  _context = context;
        //}
        private readonly UserManager<ShopUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public OrdersModel(UserManager<ShopUser> userManager, RoleManager<IdentityRole> roleManager, YourPizzaShopContext context)

        {

            _context = context;
            _userManager = userManager;
           //_roleManager = roleManager;
        }
        
        //this part of the code is pretty interesting. it allows you check if the user
        //isadmin == true, else, it gives off the order attached to a single user.

       public async Task OnGet()
       {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (!isAdmin)
                {
                    Orders = _context.PizzaOrders.Where(x => x.OwnerId == userId).ToList();
                }
                else
                {
                    Orders = _context.PizzaOrders.Include(x => x.Owner).ToList();
                   
                }
            }
        
           //var userId = await
           // _context.PizzaOrders.Remove(PizzaOrder)
       }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> OnPostDelete(int Id)
        {
            var deleteItem = await _context.PizzaOrders.FindAsync(Id);

            if (deleteItem == null)
            {
                return NotFound();
            }

            var removal = _context.PizzaOrders.Remove(deleteItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("orders");
        }

        


    }
}