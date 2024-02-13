using Online_Survey.Models;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Net_Survey.Controllers
{
    public class PersonController : Controller
    {
        SurveyEntities db = new SurveyEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Person person)
        {
            // Method çaðrýrýlýnca hemen ekleme yapmamasý için þart eklendi.
            if (person.NameSurname != null)
            {
                person.CreateDate = DateTime.Now;
                person.CreateBy = "System";

                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

    }
}
