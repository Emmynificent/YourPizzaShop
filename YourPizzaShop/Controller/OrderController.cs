using Microsoft.AspNetCore.Mvc;
using YourPizzaShop.Models;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace YourPizzaShop.Controller
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly YourPizzaShopContext _context;
        public List<PizzaOrder> Orders = new List<PizzaOrder>();
        private readonly UserManager<ShopUser> _userManager;


        public OrderController(YourPizzaShopContext context, UserManager<ShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<ActionResult<IEnumerable<PizzaOrder>>> GetPizza()
        {
            return await _context.PizzaOrders.ToListAsync();
        }

        [HttpPost]
      //  [Authorize]
        public async Task<ActionResult<PizzaOrder>> PostOrder([FromBody] PizzaOrderRequest pizzaorderRequest)
        {
            var userId = User.Identity?.Name;

            //orders = _context.PizzaOrders.Where(x => x.OwnerId == userId).ToList();

            var pizzaOrder = new PizzaOrder
            {
                 Id = pizzaorderRequest.Id,
                 PizzaName = pizzaorderRequest.PizzaName,
                 BasePrice = pizzaorderRequest.BasePrice,
                 Status = OrderStatus.New,
                 OwnerId = ""
            };

            _context.PizzaOrders.Add(pizzaOrder);
            _context.SaveChanges();
            return Ok();
               
        }


    }

}
