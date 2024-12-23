namespace MVCApplicationCore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItem { get; set;}
    }
}
