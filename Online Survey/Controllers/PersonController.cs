using System.Diagnostics;
using System.Web.Mvc;

namespace Net_Survey.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        
    }
}
