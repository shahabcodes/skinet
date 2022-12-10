namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public string? Description { get; set; }    
        public string? Price { get; set; }    
        public string? PictureUrl { get; set; }    
        public string? ProductTypeName { get; set; }
        public int ProductTypeId { get; set; }
        public string? ProductBrandName { get; set; }
        public int ProductBrandId{ get; set; }
        public int TotalRows{ get; set; }

    }
}