using Online_Survey.Models;
using System.Linq;
using System.Web.Mvc;

namespace Online_Survey.Controllers
{
    public class LoginController : Controller
    {
        SurveyEntities db = new SurveyEntities();
        public ActionResult SignIn(string Code, string Password)
        {
            if (Code == null)
            {
                return View();
            }
            else
            {
                var person = db.Person.FirstOrDefault(x => x.Code == Code && x.Password == Password);
                if (person != null)
                {
                    Session["Code"] = person.Code;
                    Session["NameSurname"] = person.NameSurname;
                    if (person.IsAdmin == true)
                    {
                        Session["Admin"] = "Admin";
                        // Method | action
                        return RedirectToAction("Index", "Person");
                    }
                    return RedirectToAction("Index", "Answer");
                }
                else
                {
                    ViewBag.Error = "Giriş Bilgileri Hatalı!";
                    return View();
                }
            }
        }

        public ActionResult LogOut()
        {
            // Session temizliyor
            Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
