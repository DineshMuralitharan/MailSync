using MailSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    /// <summary>
    /// Create class for customer list model
    /// </summary>
    public class CustomerListModel
    {
        /// <summary>
        /// To detlete customer list mapping details
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="listId">list id</param>
        /// <returns>return status</returns>
        public bool DeleteCustomerListMappingDetails(string email, int listId)
        {
            bool isSuccess = false;
            try
            {
                int customerId = CustomerModel.GetCustomerId(email);
                using (var entities = new DataEntities())
                { 
                    var customerMappingDetails = (from customerListMapping in entities.CustomerListMappings where customerListMapping.CustomerId == customerId && customerListMapping.ListId == listId select customerListMapping).FirstOrDefault();
                    customerMappingDetails.IsActive = false;
                    entities.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }

        /// <summary>
        /// To unsubscribe customer from list
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="listId">list id</param>
        /// <returns>return status</returns>
        public bool UnsubscribeCustomerFromList(string email, int listId)
        {
            bool isSuccess = false;
            try
            {
                int customerId = CustomerModel.GetCustomerId(email);
                using (var entities = new DataEntities())
                {
                    var customerMappingDetails = (from customerListMapping in entities.CustomerListMappings where customerListMapping.CustomerId == customerId && customerListMapping.ListId == listId select customerListMapping).FirstOrDefault();
                    customerMappingDetails.IsUnsubscribed = true;
                    entities.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }

        /// <summary>
        /// To add group details
        /// </summary>
        /// <param name="groupName">groupName</param>
        /// <returns></returns>
        public bool AddGroupDetails(string groupName)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                { var isGroupNameAlreadyAvailable = (from grp in entities.CustomerLists where grp.Name == groupName select grp).Any();
                    if (!isGroupNameAlreadyAvailable)
                    {
                        CustomerList customerList = new CustomerList();
                        customerList.Name = groupName;
                        customerList.CreatedDate = DateTime.Now;
                        customerList.IsActive = true;
                        entities.CustomerLists.Add(customerList);
                        entities.SaveChanges();
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return isSuccess;
        }
    }
}