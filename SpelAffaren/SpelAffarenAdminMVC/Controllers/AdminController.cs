using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpelDatabas;

namespace SpelAffarenAdminMVC.Controllers
{
    public class AdminController : BaseController
    {
        public Personer GetAdmin()
        {
            using (var db = new SpelDatabasContainer())
            {
                var admin = (from a in db.PersonerSet
                             where LoggedInAdmin.AdminStatus == true
                             select a).FirstOrDefault();
                return admin ?? new Personer();
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}