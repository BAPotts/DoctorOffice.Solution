using Microsoft.AspNetCore.Mvc;

namespace DoctorOffice.Controllers
{
  public class HomeController: Controllers
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}