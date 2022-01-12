using System.Collections.Generic;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public Recipe()
        {
            this.JoinEntities = new HashSet<RecipeTag>(); //Collection navigation property - contains a reference to many related Items. Without this property, we won't be able to access related Items in our controllers and views
        }

        public int RecipeId { get; set; }
        public string Name { get; set; }

        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public virtual ICollection<RecipeTag> JoinEntities { get; set; }
    }
}