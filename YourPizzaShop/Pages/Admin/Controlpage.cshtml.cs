using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Data;

// this page is only accessible by an admin.
// the purpose is to give the admin the permission to other users to become admins.

namespace YourPizzaShop.Pages.Admin
{
    public class ControlpageModel : PageModel
    {
        private readonly YourPizzaShopContext _context;
        public List<ShopUser> shopUsers = new List<ShopUser>();
        private readonly UserManager<ShopUser> _userManager;

        

        public ControlpageModel(YourPizzaShopContext context, UserManager<ShopUser> userManager, ShopUser shopUser)
        {
            _context = context;
            _userManager = userManager;
        }



        public async Task OnPost()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);    
            if (user != null)
            {
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (!isAdmin)
                {
                    var makeadmin = await _userManager.AddToRoleAsync(user, "Admin");
                    _context.SaveChanges();
                }

            }
        }

    }
}
