using EcommerceByAdo.Data.Enums;

namespace EcommerceByAdo.Models
{
    public class Mens
    {
        public long Id { get; set; }

        public Guid Identifier { get; set; } = Guid.NewGuid();

        public string? CName { get; set; }

        public string? CImage { get; set; }

        public int? Rate { get; set; }

        public Category? Category { get; set; }

        public Guid UserId { get; set; } = Guid.Empty;


    }
}
