namespace EcommerceByAdo.Models
{
    public class ProductATC
    {
        public int id { get; set; }

        public Guid ProductCartID { get; set; } = Guid.NewGuid();
        
        public Guid MensGuid { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; } = 1;

        public DateTime Date { get; set; } = DateTime.Now;

        public Guid OrderID { get; set; } = Guid.Empty;
    }
}
