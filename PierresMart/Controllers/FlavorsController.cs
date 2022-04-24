using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierresMart.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PierresMart.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly PierresMartContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public FlavorsController(UserManager<ApplicationUser> userManager, PierresMartContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userFlavors);
    }

    public async Task<ActionResult> Create()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.TreatId = new SelectList(_db.Treats.Where(entry => entry.User.Id == currentUser.Id), "TreatId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor, int TreatId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() {TreatId = TreatId, FlavorId = flavor.FlavorId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Flavor foundFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(foundFlavor);
    }

    public ActionResult Edit(int id)
    {
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(foundFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() {TreatId = TreatId, FlavorId = flavor.FlavorId});
      }
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(foundFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(foundFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddTreat(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      ViewBag.TreatId = new SelectList(_db.Treats.Where(entry => entry.User.Id == currentUser.Id), "TreatId", "Name");
      Flavor foundFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(foundFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.FlavorTreats.Add(new FlavorTreat() {TreatId = TreatId, FlavorId = flavor.FlavorId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId)
    {
        var joinEntry = _db.FlavorTreats.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
        _db.FlavorTreats.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}