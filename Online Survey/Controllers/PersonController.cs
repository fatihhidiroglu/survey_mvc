using Online_Survey.Models;
using Online_Survey.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Online_Survey.Controllers
{
    public class PersonController : BaseController
    {
        public ActionResult Index()
        {
            // debug daha kolay yapabilmek i�in de�i�kene atand�
            var model = db.Person.ToList();
            return View(model);
        }

        public ActionResult Create(Person person, string Answer)
        {
            // Method �a�r�r�l�nca hemen ekleme yapmamas� i�in �art eklendi.
            if (person.NameSurname != null)
            {
                person.CreateDate = DateTime.Now;
                person.CreateBy = NameSurname;
                if (Answer == Constants.AnswerType.Yes)
                {
                    person.IsAdmin = true;
                }
                else
                {
                    person.IsAdmin = false;
                }
                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // ? null olabilir anlam�nda kullan�l�yor
        public ActionResult Edit(int? Id)
        {
            // Method �a�r�r�l�nca hemen ekleme yapmamas� i�in �art eklendi.
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var model = db.Person.Find(Id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Person person)
        {
            // de�i�tirilmemesi gerekenleri burada belirtiyoruz.
            db.Entry(person).State = System.Data.Entity.EntityState.Modified;
            db.Entry(person).Property(e => e.CreateBy).IsModified = false;
            db.Entry(person).Property(e => e.CreateDate).IsModified = false;

            person.ModifyBy = NameSurname;
            person.ModifyDate = DateTime.Now;
            // G�ncelleme i�leminde hangi id g�ncellenecekse iletilmesi gerekli yoksa hata al�n�r.
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var person = db.Person.Find(Id);
            db.Person.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
