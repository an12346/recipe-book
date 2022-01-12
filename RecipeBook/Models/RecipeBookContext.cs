using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeBook.Models
{
  public class RecipeBookContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RecipeTag> RecipeTag { get; set; } //each DbSet will become a table in our database
  
    public RecipeBookContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}