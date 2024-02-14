using Online_Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Survey.Utils
{
    public class BaseController : Controller
    {
        // Diğer controller'ın erişebilmesi için public olarak oluşturuldu.
        public SurveyEntities db = new SurveyEntities();
        public string Code { get; set; }
        public string NameSurname { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Code"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/SignIn");
            }
            else
            {
                Code = Session["Code"].ToString();
                NameSurname = Session["NameSurname"].ToString();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}