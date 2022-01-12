using System.Collections.Generic;

namespace RecipeBook.Models
{
  public class Tag
  {
    public Tag()
    { 
      this.JoinEntities = new HashSet<RecipeTag>();
    }
    public int TagId { get; set; }
    public string Description { get; set; }
    public virtual ApplicationUser User { get; set; } 
    public virtual ICollection<RecipeTag> JoinEntities { get;}
  }
}