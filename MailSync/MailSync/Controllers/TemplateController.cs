using MailSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSync.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index()
        {
            ViewBag.TemplateDetails = TemplateModel.GetTemplateDetails();
            return View();
        }
    }
}