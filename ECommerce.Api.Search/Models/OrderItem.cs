namespace ECommerce.Api.Search.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quanity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
