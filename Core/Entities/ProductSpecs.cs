namespace Core.Entities
{
    public class ProductSpecs
    {
        public int? brandId { get; set; } = 0;
        public int? typeId { get; set; } = 0;
        public int? PageId { get; set; } = 0;
        public int? rows { get; set; } = 6;

        public string? SearchText { get; set; }
        public string? sort { get; set; } = "NULL";          
    }
}