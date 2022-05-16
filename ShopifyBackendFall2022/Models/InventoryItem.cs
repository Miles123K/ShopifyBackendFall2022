namespace ShopifyBackendFall2022.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? LocationId { get; set; }

        public string? Category { get; set; }
        public InventoryItem(int id, string name, int? locationId = null, string? category = null)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Category = category;
        }
    }
}
