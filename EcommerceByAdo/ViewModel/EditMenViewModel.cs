using EcommerceByAdo.Data.Enums;

namespace EcommerceByAdo.ViewModel
{
    public class EditMenViewModel
    {
        public string CName { get; set; }

        public IFormFile CImage { get; set; }

        public int? Rate { get; set; }
       
        public Category? Category { get; set; }

        public string? URL { get; set; }
    
    }
}
