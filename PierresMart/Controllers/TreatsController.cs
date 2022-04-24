using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierresMart.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresMart.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresMartContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresMartContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTreats);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat Treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Treat.User = currentUser;
      _db.Treats.Add(Treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat{ TreatId = Treat.TreatId, FlavorId = FlavorId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Treat foundTreat = _db.Treats
        .Include(Treat => Treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(model => model.TreatId == id);
      return View(foundTreat);
    }

    public ActionResult Edit(int id)
    {
      Treat foundTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      return View(foundTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat Treat)
    {
      _db.Entry(Treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = Treat.TreatId});
    }

    public ActionResult Delete(int id)
    {
      Treat foundTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      return View(foundTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat foundTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      _db.Treats.Remove(foundTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddFlavor(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.FlavorId = new SelectList(_db.Flavors.Where(entry => entry.User.Id == currentUser.Id), "FlavorId", "Name");
      Treat foundTreat = _db.Treats.FirstOrDefault(model => model.TreatId == id);
      return View(foundTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat Treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat{ TreatId = Treat.TreatId, FlavorId = FlavorId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int id)
    {
      var joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == id);
      _db.FlavorTreats.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}