using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourPizzaShop.Models;
using YourPizzaShop.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using YourPizzaShop.Pages.Checkout;
using Microsoft.AspNetCore.Authorization;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Pages;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourPizzaShop.Pages
{
    [BindProperties(SupportsGet = true)]
    [Authorize(Roles = "Admin")]

    public class WaitPage : PageModel
    {
        [BindProperty]
        public PizzaOrder Order { get; set; }
        


        private readonly YourPizzaShopContext _context;
        private readonly UserManager<ShopUser> _users;
        private readonly YourPizzaShop.Services.IMailService _mailer;

        public List<PizzaOrder> orders { get; set; } = new List<PizzaOrder>();

        public WaitPage(YourPizzaShopContext context , Services.IMailService mailer, UserManager<ShopUser> users)
        {
            _context = context;
            _mailer = mailer;
            _users = users;
           
        }

        public async Task<IActionResult> OnGet(int Id)
        {
          

            var updateStatus = await _context.PizzaOrders.FindAsync(Id);
            //updateStatus.Owner.Email

            if (Id != 0) 
            {
                //var 
                orders = await _context.PizzaOrders.Where(o => o.Id == Id).ToListAsync();
                if (orders != null)
                {
                    
                    //await _context.SaveChangesAsync();

                    return Page();

                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            int id;
            //var users = _users.Users;
            //int id;
      


            if (int.TryParse(Request.Form["Id"], out id) && Enum.TryParse<OrderStatus>(Request.Form["newStatus"], out var statusEnum))
            {
                //this part allows me access the email attached to the order whose status is about to be updated;
                var person = await _context.PizzaOrders
                .Include(po => po.Owner)
                .FirstOrDefaultAsync(po => po.Id == id);
                var updateorders = await _context.PizzaOrders.FindAsync(id);


                if (updateorders == null)
                {
                    return NotFound();
                }
                else
                {

                    var mail = person.Owner.Email;

                    var firstname = person.Owner.FirstName;
                    var stat = updateorders.Status = statusEnum;

                    var save = await _context.SaveChangesAsync();
                    _context.PizzaOrders.ToList();

                    if (save > 0)
                    {
                        var mailin = new MailData(
                        to: new List<string> {mail},
                        subject: "Order's Status",
                        body: $"Dear {firstname}, kindly be informed that the status of your order has been updated to {stat}!",
                        from: "genz.greatness@gmail.com",
                        displayName: "The Pizza House");

                        await _mailer.SendAsync(mailin, new CancellationToken());


                    }
                    return Redirect("Orders");
                }
            }
            return Page();
        }
    }
}

//    await _mailer.SendAsync(mailin, new CancellationToken());

//    var orderList = _context.PizzaOrders.ToList();
//    return Redirect("Orders");

//}

//public async Task<IActionResult> OnPostSelectOrder(int Id, string newStatus)
//{
//    var updateorders = await _context.PizzaOrders.FindAsync(Id);
//    if (updateorders == null)
//    {
//        return NotFound();

//    }
//    else
//    {
//        if (Enum.TryParse<OrderStatus>(newStatus, out var statusEnum))
//        {
//            updateorders.Status = statusEnum;

//            await _context.SaveChangesAsync();
//            var orderList = _context.PizzaOrders.ToList();

//            return Redirect("Orders");

//        }

//        return Page();
//    }
//}


