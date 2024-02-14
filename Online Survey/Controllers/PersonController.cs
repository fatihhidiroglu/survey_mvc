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
            // debug daha kolay yapabilmek için deðiþkene atandý
            var model = db.Person.ToList();
            return View(model);
        }

        public ActionResult Create(Person person, string Answer)
        {
            // Method çaðrýrýlýnca hemen ekleme yapmamasý için þart eklendi.
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

        // ? null olabilir anlamýnda kullanýlýyor
        public ActionResult Edit(int? Id)
        {
            // Method çaðrýrýlýnca hemen ekleme yapmamasý için þart eklendi.
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
            // deðiþtirilmemesi gerekenleri burada belirtiyoruz.
            db.Entry(person).State = System.Data.Entity.EntityState.Modified;
            db.Entry(person).Property(e => e.CreateBy).IsModified = false;
            db.Entry(person).Property(e => e.CreateDate).IsModified = false;

            person.ModifyBy = NameSurname;
            person.ModifyDate = DateTime.Now;
            // Güncelleme iþleminde hangi id güncellenecekse iletilmesi gerekli yoksa hata alýnýr.
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
