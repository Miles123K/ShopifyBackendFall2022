namespace ShopifyBackendFall2022.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Location { get; set; }

        public string? Category { get; set; }
        public InventoryItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
