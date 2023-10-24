using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using YourPizzaShop.Models;



//this is a fakedatabase for our pizzas
namespace YourPizzaShop.Pages
{
    [Authorize]
    public class pizzaModel : PageModel
    {
        public List<PizzasModel> fakePizzaDB = new List<PizzasModel>()
        {

            new PizzasModel(){ImageTitle="Margerita", PizzaName="Margerita", BasePrice=2,
                TomatoSauce = true,
                Cheese=true,
                FinalPrice = 6},

            new PizzasModel(){ImageTitle="Bolognese", PizzaName="Bolognese", BasePrice=2,
                Beef=true, Ham=true,
                Cheese=true,
                FinalPrice = 6},

            new PizzasModel(){ImageTitle="Carbonara", PizzaName="Carbonara", BasePrice=2,
                TomatoSauce = true, Beef=true,Tuna=true,Peperoni=true,
                Cheese=true,
                FinalPrice = 7},

            new PizzasModel(){ImageTitle="Hawaiian", PizzaName="Hawaiian", BasePrice=2,
                TomatoSauce = true, Tuna=true, Beef=true,
                Cheese=true,
                FinalPrice = 4},

            new PizzasModel(){ImageTitle="MeatFeast", PizzaName="MeatFeast", BasePrice=8,Cheese=true, TomatoSauce=true,
            Ham=true, FinalPrice =9}

        };

        public void OnGet()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //string username = User.Identity.Name;

            //var userEmail = User.FindFirstValue(ClaimTypes.Email);
            //var userRoles = User.FindAll(ClaimTypes.Role);
        }
    }
}
