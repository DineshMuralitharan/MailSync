using MailSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSync.Controllers
{
    /// <summary>
    /// Create class for API controller
    /// </summary>
    public class APIController : Controller
    {
        /// <summary>
        /// To add customer details
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult AddCustomers()
        {
            string customerName = Request.Headers.GetValues("CustomerName").FirstOrDefault();
            string customerEmail = Request.Headers.GetValues("CustomerEmail").FirstOrDefault();
            string dateofBirth = Request.Headers.GetValues("DOB").FirstOrDefault();
            DateTime dob = Convert.ToDateTime(dateofBirth);
            string listid = Request.Headers.GetValues("ListId").FirstOrDefault();
            int listId = int.Parse("listid");
            CustomerModel customerModel = new CustomerModel();
            bool isSuccess = customerModel.AddCustomerDetails(customerName, customerEmail, dob, listId);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// To delete customers
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult DeleteCustomers()
        {
            string customerEmail = Request.Headers.GetValues("CustomerEmail").FirstOrDefault(); 
            CustomerModel customerModel = new CustomerModel();
            bool isSuccess = customerModel.DeleteCustomerDetails(customerEmail);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// To delete customer from list
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult DeleteCustomersListMapping()
        {
            string email = Request.Headers.GetValues("CustomerEmail").FirstOrDefault();
            string listid = Request.Headers.GetValues("ListId").FirstOrDefault();
            int listId = int.Parse("listid");
            CustomerListModel customerListModel = new CustomerListModel();
            bool isSuccess = customerListModel.DeleteCustomerListMappingDetails(email, listId);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// To unsubscribe customer from list
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult UnSubscribeCustomersFromListMapping(string email, int listId)
        { 
            CustomerListModel customerListModel = new CustomerListModel();
            bool isSuccess = customerListModel.UnsubscribeCustomerFromList(email, listId);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// To unsubsribe customer from all
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult UnSubcribeAll(string email)
        { 
            CustomerModel customerModel = new CustomerModel();
            bool isSuccess = customerModel.UnsubscribeAll(email);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// To add cart details
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult AddCardDetails()
        { 
            string customerEmail = Request.Headers.GetValues("CustomerEmail").FirstOrDefault();
            string productDetails = Request.Headers.GetValues("ProductDetails").FirstOrDefault(); 
            CartModel cartModel = new CartModel();
            bool isSuccess = cartModel.AddCartDetais(customerEmail, productDetails);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// To add group details
        /// </summary>
        /// <returns>return json result</returns>
        public ActionResult AddGroup()
        { 
            string customerName = Request.Headers.GetValues("CustomerName").FirstOrDefault(); 
            CustomerListModel customerModel = new CustomerListModel();
            bool isSuccess = customerModel.AddGroupDetails(customerName);
            if (isSuccess)
            {
                return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { ValidationErrorMessage = "An unknown error has occurred. Please try again.", IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}