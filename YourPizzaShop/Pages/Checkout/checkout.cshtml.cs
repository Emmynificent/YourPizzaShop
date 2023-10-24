using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using System.Threading;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Data;
using YourPizzaShop.Models;
using YourPizzaShop.Services;
//using MailKit.Net.Smtp;
//using YourPizzaShop.Pages.Checkout.


namespace YourPizzaShop.Pages.Checkout
{
    [BindProperties(SupportsGet = true)]
    public class checkoutModel : PageModel
    {

        //private readonly UserManager<ShopUser> _userManager;

        public string PizzaName { get; set; }

        public float PizzaPrice { get; set; }

        public string ImageTitle { get; set; }


        private readonly YourPizzaShopContext _context;

        private readonly UserManager<ShopUser> _userManager;

        private readonly YourPizzaShop.Services.IMailService _mailService;
         

        public checkoutModel(YourPizzaShopContext context, UserManager<ShopUser> userManager, YourPizzaShop.Services.IMailService mailService)
        {
            _context = context;
            _userManager = userManager;
            _mailService = mailService; 
            
        }

        public async void OnGet()
        {
            if (string.IsNullOrWhiteSpace(PizzaName))
            {
                PizzaName = ImageTitle;
            }
            else
            {

            }
            if (string.IsNullOrWhiteSpace(ImageTitle))
            {
                ImageTitle = "Create";
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //orders = _context.PizzaOrders.Where(x => x.OwnerId == userId).ToList();

            PizzaOrder pizzaOrder = new PizzaOrder();

            pizzaOrder.PizzaName = PizzaName;

            pizzaOrder.BasePrice = PizzaPrice;

            pizzaOrder.OwnerId = userId;

            _context.PizzaOrders.Add(pizzaOrder);

            var completed = _context.SaveChanges();

            if (completed > 0)
            {
                var users = await _userManager.FindByIdAsync(userId);
                if (users != null)
                {
                    var isAdmin = await _userManager.IsInRoleAsync(users, "Admin");
                    if (!isAdmin)
                    {
                        var usermail = users.Email;
                        var mailing = new MailData(

                            to: new List<string> { usermail },
                            subject: "Order Confirmation Mail",
                            body: $"Kindly be informed that your order {pizzaOrder.PizzaName} is being processed.",
                            from: "genz.greatness@gmail.com",
                            displayName: "The PizzaHouse");



                        await _mailService.SendAsync(mailing, new CancellationToken());
                    }
                    
                }



            }

        }
    }
}

            //if(completed.State == EntityState.Added)
            //{
            //    var users = await _userManager.FindByIdAsync(userId);

            //    if(users != null)
            //    {
            //        var useremail = users.Email;
            //        var mailing = new MailData(

            //            to: new List<string> { useremail },

            //            subject: "Order Confirmation Mail",
            //            body: $"Kindly be informed that your order {pizzaOrder.PizzaName} is being processed.",
            //            from: $"samanta89@ethereal.email",
            //            displayName: "The PizzaHouse",
            //            replyTo: "", replyToName: "");

            //    }
            //    else
            //    {

            //    }

            
        

    
            
            


           


           // try
            //{
              //  _context.SaveChanges();
                //return RedirectToPage("/Index"); // Redirect to a success page
            //}
            //catch (Exception ex)
            //{
                // Handle the exception and log it
              //  ModelState.AddModelError("", "An error occurred while saving the order.");
                //return RedirectToPage("Orders");
            //}
           // _context.SaveChanges();

        
    
