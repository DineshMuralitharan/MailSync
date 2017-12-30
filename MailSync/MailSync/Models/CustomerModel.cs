using MailSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    /// <summary>
    /// Create class for customer model
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// To add customer details
        /// </summary>
        /// <param name="name">customer name</param>
        /// <param name="email">customer email</param>
        /// <param name="dateOfBirth">date of birth</param>
        /// <returns>return status</returns>
        public bool AddCustomerDetails(string name, string email, DateTime dateOfBirth, int listId)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                {
                    Customer customer = new Customer();
                    customer.Name = name;
                    customer.Email = email;
                    customer.DateOfBirth = dateOfBirth;
                    customer.CreatedDate = DateTime.Now;
                    customer.IsUnsubscribed = false;
                    customer.IsActive = true;
                    entities.Customers.Add(customer);

                    CustomerListMapping customerListMapping = new CustomerListMapping();
                    customerListMapping.CustomerId = customer.Id;
                    customerListMapping.ListId = listId;
                    customerListMapping.IsUnsubscribed = false;
                    customerListMapping.CreatedDate = DateTime.Now;
                    customerListMapping.ModifiedDate = DateTime.Now;
                    customerListMapping.IsActive = true;
                    entities.CustomerListMappings.Add(customerListMapping);
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
        /// To delete customer details
        /// </summary>
        /// <param name="email">customer email</param>
        /// <returns>return status</returns>
        public bool DeleteCustomerDetails(string email)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                {
                    var customerDetails = (from customer in entities.Customers where customer.Email == email select customer).FirstOrDefault();
                    customerDetails.IsActive = false;
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
        /// To unsubscribe customer details
        /// </summary>
        /// <param name="email">customer email</param>
        /// <returns>return status</returns>
        public bool UnsubscribeAll(string email)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                {
                    var customerDetails = (from customer in entities.Customers where customer.Email == email select customer).FirstOrDefault();
                    customerDetails.IsUnsubscribed = true;
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
        /// To get customer id based on email
        /// </summary>
        /// <param name="email">customer email</param>
        /// <returns>return customer id</returns>
        public static int GetCustomerId(string email)
        {
            int customerId = 0;
            try
            {
                using (var entities = new DataEntities())
                {
                    customerId = (from customer in entities.Customers where customer.Email == email select customer.Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return customerId;
        }
    }
}