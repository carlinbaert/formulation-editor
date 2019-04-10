using Microsoft.EntityFrameworkCore;

namespace IngredientPricingAPI.Models
{
    public class IngredientPriceContext : DbContext
    {
        public IngredientPriceContext(DbContextOptions<IngredientPriceContext> options)
            : base(options)
        {}

        public DbSet<IngredientPrice> IngredientPrices { get; set; }
    }
}
