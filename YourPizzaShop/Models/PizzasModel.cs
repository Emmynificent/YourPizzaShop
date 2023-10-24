namespace YourPizzaShop.Models
{
    public class PizzasModel
    {
        public string ImageTitle { get; set; }
        public string PizzaName { get; set; }
        public float BasePrice { get; set; } = 2;
        public bool TomatoSauce { get; set;}
        public bool Cheese { get; set; }
        public bool Peperoni { get; set; }
        public bool Tuna { get; set; }
        public bool Ham { get; set; }
        public bool Dodo { get; set;}
        public bool Chicken { get; set;}
        public bool Beef { get; set; }
        public bool Spagetti { get; set; }
        public float FinalPrice { get; set; }

    }

}
