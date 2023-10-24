using System.ComponentModel.DataAnnotations.Schema;
using YourPizzaShop.Areas.Identity.Data;


using System.ComponentModel.DataAnnotations;

namespace YourPizzaShop.Models
{
    public enum OrderStatus
    {
        New,
        Approved,
        Pending,
        Completed,
        Cancelled,
    }

    public class PizzaOrder
    {

        public int Id { get; set;}
        public string PizzaName { get; set; } 
        public float BasePrice { get; set; }
        public OrderStatus Status { get; set; } 
        public string OwnerId { get; set; }
        public ShopUser Owner { get; set; }


        public PizzaOrder()
        {
            Status = OrderStatus.New;
        }
    }

}