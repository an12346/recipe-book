using Microsoft.AspNetCore.Mvc;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;


namespace RecipeBook.Controllers
{
  [Authorize]
  public class TagsController : Controller
  {
    private readonly RecipeBookContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TagsController(UserManager<ApplicationUser> userManager, RecipeBookContext db)
    {
      _db = db;
      _userManager = userManager;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTags = _db.Tags.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTags);
    }

    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View();
    }

     [HttpPost]
    public async Task<ActionResult> Create(Tag tag, int RecipeId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      tag.User = currentUser;
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
      var thisTag = _db.Tags
          .Include(tag => tag.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

     public ActionResult Edit(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(Tag => Tag.TagId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisTag);
    }


    [HttpPost]
    public ActionResult Edit(Tag tag)
    {
      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = tag.TagId });
    }

     public ActionResult Delete(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
      return View(thisTag);
    }

     [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
      _db.Tags.Remove(thisTag);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
