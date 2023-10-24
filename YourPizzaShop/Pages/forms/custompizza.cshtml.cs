using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YourPizzaShop.Models;

namespace YourPizzaShop.Pages.forms
{
    public class custompizzaModel : PageModel
    {
        [BindProperty]//this allows us access our model//
        public PizzasModel Pizza { get; set; }
        public float PizzaPrice { get; set; } 

        public void OnGet()
        {
        }

        //IActionResult handles request n response
        public IActionResult OnPost()
        {
            PizzaPrice = Pizza.BasePrice;


            if (Pizza.TomatoSauce) PizzaPrice += 1;
            if (Pizza.Cheese ) PizzaPrice += 1;
            if (Pizza.Peperoni ) PizzaPrice += 1;
            if (Pizza.Ham)PizzaPrice += 1;
            if (Pizza.Beef )PizzaPrice += 1;
            if (Pizza.Tuna) PizzaPrice += 1;
            if (Pizza.Chicken) PizzaPrice += 1;
            if (Pizza.Dodo) PizzaPrice += 1;
            if (Pizza.Spagetti) PizzaPrice += 1;


            return RedirectToPage("/Checkout/Checkout", new {Pizza.PizzaName, PizzaPrice});
        }
    }

}