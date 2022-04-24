using Microsoft.AspNetCore.Mvc;

namespace PierresMart.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}