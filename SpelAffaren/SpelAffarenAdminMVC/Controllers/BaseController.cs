using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpelDatabas;

namespace SpelAffarenAdminMVC.Controllers
{
    public class BaseController : Controller
    {
        public Personer LoggedInAdmin
        {
            get
            {
                return (Personer)Session["admin"];
            }
        }
        //public Admin LoggedInAdmin
        //{
        //    get
        //    {
        //        return (Admin)Session["admin"];
        //    }
        //}
    }
}