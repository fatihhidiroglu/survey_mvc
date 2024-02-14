using Online_Survey.Models;
using Online_Survey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Survey.Controllers
{
    public class AnswerController : BaseController
    {
        public ActionResult Index()
        {
            var model = db.Answer.Where(m=>m.UserCode == UserCode).ToList();
            return View(model);
        }
        public ActionResult Create(string Code)
        {
            if (Code == null)
            {
                List<SelectListItem> personList = (from person in db.Person
                                                   where person.Code != UserCode
                                                   select new SelectListItem
                                                   {
                                                       Text = person.NameSurname,
                                                       Value = person.Code.ToString()
                                                   }).ToList();

                ViewBag.Person = new SelectList(personList.OrderBy(m => m.Text), "Value", "Text");

                var questionModel = db.Question.ToList();
                return View(questionModel);
            }
            else
            {
                CalculateScore(Code);
                return RedirectToAction("Index");
            }
        }
        public void CalculateScore(string code)
        {
            double answerYes = 0, answerNo = 0, scoreResult = 0;
            var answer = db.Answer.FirstOrDefault(m => m.PersonCode == code && m.UserCode == UserCode);
            var answerLine = db.AnswerLine.Where(m => m.AnswerId == answer.Id).ToList();

            foreach (var item in answerLine)
            {
                if (item.Answer == Constants.AnswerType.Yes)
                {
                    answerYes++;
                }
                else
                {
                    answerNo++;
                }
            }
            scoreResult = (answerYes / (answerYes + answerNo)) * 100;
            if (scoreResult > 69)
            {
                answer.IsComplete = true;
            }
            else
            {
                answer.IsComplete = false;
            }
            answer.Score = scoreResult.ToString();
            db.SaveChanges();
        }
        public String SendData(AnswerModel answerModel)
        {
            int? month = DateTime.Now.Month;
            var model = db.Answer.FirstOrDefault(m => m.PersonCode == answerModel.Code && m.UserCode == UserCode && m.CreateDate.Value.Month == month);

            if (model != null)
            {
                SaveAnswerLine(model.Id, answerModel.Answer, answerModel.Question);
            }
            else
            {
                Answer answer = new Answer();
                answer.PersonCode = answerModel.Code;
                answer.PersonName = answerModel.NameSurname;
                answer.UserCode = UserCode;
                answer.CreateDate = DateTime.Now;
                answer.CreateBy = NameSurname;

                db.Answer.Add(answer);
                db.SaveChanges();
                SaveAnswerLine(answer.Id, answerModel.Answer, answerModel.Question);
            }
            return "True";
        }
        public void SaveAnswerLine(int answerId, string answer, string question)
        {
            var model = db.AnswerLine.FirstOrDefault(m => m.AnswerId == answerId && m.Question == question);

            if (model != null)
            {
                model.Answer = answer;
                db.SaveChanges();
            }
            else
            {
                AnswerLine answerLine = new AnswerLine();
                answerLine.AnswerId = answerId;
                answerLine.Answer = answer;
                answerLine.Question = question;

                db.AnswerLine.Add(answerLine);
                db.SaveChanges();
            }

        }
    }
}