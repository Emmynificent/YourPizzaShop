using YourPizzaShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using YourPizzaShop.Areas.Identity.Data;
using Microsoft.OpenApi.Models;
using YourPizzaShop.Configuration;
using YourPizzaShop.Services;
using Microsoft.AspNetCore.Builder;
//[Microsoft.AspNetCore.Mvc.HttpGet]


public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        //builder.Services.AddRazorPages();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<YourPizzaShopContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("YourPizzaShopContextConnection"));
        });

       builder.Services.AddIdentity<ShopUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
        
        .AddEntityFrameworkStores<YourPizzaShopContext>()
        .AddDefaultTokenProviders()
        .AddDefaultUI();


        // another smtp related service

       // email UI tester;
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Example API",
                Version = "v1",
                Description = "An example of an ASP.NET core Web API",
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Email = "example@gmail.com",
                    Url = new Uri("https://example.com/contact"),
                },


            });
        });
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

        builder.Services.AddTransient<IMailService, MailService>();
   
        builder.Services.AddRazorPages();
        builder.Services.AddMvc();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //email testing related;
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.MapRazorPages();

      


        //not sure I reall understand what is happening here.
        using (var scope = app.Services.CreateScope())
       {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Manager", "Customer" };

            foreach (var role in roles)
            {

              if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

            }
        }
       using (var scope = app.Services.CreateScope())
        {
           var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ShopUser>>();
        
            string email = "admin@admin.com";
            string password = "Unknown@1234";
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ShopUser();
                user.UserName = email;
                user.Email = email;
                user.FirstName = "Admin";
                user.LastName = "User";
                user.PhoneNumber = "080111111111";

                var test = await userManager.CreateAsync(user, password);
                if (test.Succeeded)
                {
                    var test2 = await userManager.AddToRoleAsync(user, "Admin");

                }
                await userManager.CreateAsync(user, password);


            }

              app.Run();
        }


    }
}

    
