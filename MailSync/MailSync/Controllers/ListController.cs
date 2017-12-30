using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using MailSync.Models;
using Syncfusion.JavaScript;

namespace MailSync.Controllers
{
    public class ListController : Controller
    {
        [Route("lists/{listId:int}")]
        // GET: List
        public ActionResult List(int listId)
        {
            var list = new ListModel();
            var lisDetails = list.GetListDetails(listId);
            return View(lisDetails);
        }

        [Route("lists/details/{listId:int}")]
        public JsonResult ListDetails(int listId, [System.Web.Http.FromBody]DataManager dataManager)
        {
            var listModel = new ListModel();
            var list = listModel.GetCustomerMappedlist(listId, dataManager);
            return this.Json(new { result = list.CustomerMappedListDetails, count = listModel.TotalListCount },JsonRequestBehavior.AllowGet);
        }


        [Route("lists/userdelete/{listId:int}/{userId:int}")]
        public JsonResult DeleteUserFromGroup(int listId, int userId)
        {
            return this.Json(new { data = ListModel.DeleteUserFromList(listId, userId) });
        }

        public ActionResult Group()
        {
            ListModel listDetails = new ListModel();
            listDetails.groupListValue = listDetails.GetListDetails();
            return View(listDetails);
        }

        [Route("group/add")]
        public JsonResult AddGroup(string groupName)
        {
            return this.Json(new { data = ListModel.AddGroupDetails(groupName) });
        }
    }
}