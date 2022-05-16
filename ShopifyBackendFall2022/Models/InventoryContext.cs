using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


namespace ShopifyBackendFall2022.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) :
            base(options)
        { }


        public DbSet<InventoryItem>? inventoryItems { get; set; }
    }
}
