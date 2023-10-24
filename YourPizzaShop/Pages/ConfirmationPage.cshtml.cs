using Microsoft.AspNetCore.Mvc;
using YourPizzaShop.Data;
using YourPizzaShop.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace YourPizzaShop.Pages
{
    public class ConfirmationPageModel : PageModel
    {
        private readonly YourPizzaShopContext _context;
        public ConfirmationPageModel(YourPizzaShopContext context)
        {
            _context = context;
        }
        [BindProperty]
        public PizzaOrder OrderedPizza { get; set; }
        public List<PizzaOrder> PizzaOrders { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            var orders = await _context.PizzaOrders.FindAsync(Id);
            if (Id != 0)
            {
                PizzaOrders = await _context.PizzaOrders.Where(x => x.Id == Id).ToListAsync();
                if (PizzaOrders != null)
                {
                    return Page();   
                }
                else { }
            }
            return Page();
        }
    }
}
