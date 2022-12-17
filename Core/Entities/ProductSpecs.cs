namespace Core.Entities
{
    public class ProductSpecs
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public int? PageId { get; set; } = 0;
        public int? rows { get; set; } 

        public string? SearchText { get; set; }
        public string? sort { get; set; } = "NULL";          
    }
}