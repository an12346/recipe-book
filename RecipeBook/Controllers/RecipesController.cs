using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecipeBook.Controllers
{
  public class RecipesController : Controller
  {
    private readonly RecipeBookContext _db;

    public RecipesController(RecipeBookContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Recipe> model = _db.Recipes.ToList();
      return View(model);
    }

     public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name", "Ingredients", "Instructions");
      return View();
    }

   [HttpPost]
    public ActionResult Create(Tag tag, int RecipeId)
    {
      _db.Tags.Add(tag);
      _db.SaveChanges();
      if (RecipeId != 0)
      {
        _db.RecipeTag.Add(new RecipeTag() { RecipeId = RecipeId, TagId = tag.TagId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisRecipe = _db.Recipes
          .Include(recipe => recipe.JoinEntities)
          .ThenInclude(join => join.Tag)
          .FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    public ActionResult Edit (int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe recipe)
    {
      _db.Entry(recipe).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }

     public ActionResult Delete(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Description");
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddTag(Recipe recipe, int TagId)
    {
      if (TagId != 0)
      {
        _db.RecipeTag.Add(new RecipeTag() { TagId = TagId, RecipeId = recipe.RecipeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = recipe.RecipeId });
    }

    [HttpPost]
    public ActionResult DeleteTag(int joinId, int recipeId)
    {
      var joinEntry = _db.RecipeTag.FirstOrDefault(joinEntry => joinEntry.RecipeTagId == joinId);
      _db.RecipeTag.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = recipeId });
    }

  }
}
