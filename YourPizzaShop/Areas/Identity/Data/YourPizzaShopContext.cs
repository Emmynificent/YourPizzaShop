using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YourPizzaShop.Areas.Identity.Data;
using YourPizzaShop.Models;


//kindly note that the DBContext, is responsible for querying, saving and basically communicating directly with the dAtaBase
namespace YourPizzaShop.Data;

public class YourPizzaShopContext : IdentityDbContext<ShopUser>
{
    public YourPizzaShopContext(DbContextOptions<YourPizzaShopContext> options)
        : base(options)
    {
    }
    public DbSet<PizzaOrder> PizzaOrders { get; set; }
    //public DbSet<PizzasModel> Pizzas { get; set; }
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //{

    //}
    //public object PizzaOrders { get; internal set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

//public class PizzaDbContext : DbContext
//{
    //public DbSet<UserProfile> UserProfiles { get; set; }    
    
//}
