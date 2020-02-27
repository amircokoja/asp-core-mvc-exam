using Microsoft.AspNetCore.Mvc;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class AjaxTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AjaxTestAction()
        {
            return PartialView("_AjaxTestView");
        }


        public ActionResult TestA()
        {
            return RedirectToAction("TestB");
        }

        public ActionResult TestB()
        {
            return RedirectToAction("TestA");
        }
    }
}