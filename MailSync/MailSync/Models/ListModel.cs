using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailSync.Base;
using MailSync.Data;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using System.Web.Mvc;

namespace MailSync.Models
{
    public class ListModel
    {
        internal int TotalListCount { get; set; }
        public List<ListDetails> groupListValue = new List<ListDetails>();
        /// <summary>
        /// Get list details based on list ID
        /// </summary>
        /// <param name="listId">list value</param>
        /// <returns>return list details</returns>
        public ListDetails GetListDetails(int listId)
        {
            ListDetails listDetails = new ListDetails();
            try
            {
                using (var context = new DataEntities())
                {
                    listDetails = (from list in context.CustomerLists
                                   where list.Id == listId && list.IsActive
                                   select new ListDetails
                                   {
                                       ListID = list.Id,
                                       ListName = list.Name,
                                       CreatedOn = list.CreatedDate
                                   }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

            }

            return listDetails;
        }

        /// <summary>
        /// Get all customer list
        /// </summary>
        /// <returns>return all active list</returns>
        public List<ListDetails> GetListDetails()
        {
            List<ListDetails> listDetails = new List<ListDetails>();
            try
            {
                using (var context = new DataEntities())
                {
                    listDetails = (from list in context.CustomerLists
                                   where list.IsActive
                                   select new ListDetails
                                   {
                                       ListName = list.Name,
                                       CreatedOn = list.CreatedDate,
                                       ListID = list.Id,
                                       CustomerCount = (from listMapping in context.CustomerListMappings
                                                        where listMapping.ListId == list.Id && listMapping.IsActive && !listMapping.IsUnsubscribed
                                                        select listMapping.Id).Count(),
                                       OpenCount = (from listMapping in context.CustomerListMappings
                                                    join emailStatistics in context.EmailStatistics on listMapping.CustomerId equals emailStatistics.CustomerId
                                                    where list.Id == listMapping.ListId && list.IsActive && emailStatistics.IsActive && emailStatistics.IsOpen
                                                    select emailStatistics.IsOpen).Count()
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return listDetails;
        }

        /// <summary>
        /// Getting all active list
        /// </summary>
        /// <returns>Return all active list</returns>
        public SelectList GetListName()
        {
            using (var context = new DataEntities())
            {
                Dictionary<int, string> listName;
                try
                {
                    listName = (from list in context.CustomerLists
                                where list.IsActive
                                select new
                                {
                                    Key = list.Id,
                                    Value = list.Name,
                                }).Distinct().ToDictionary(a => a.Key, a => a.Value);
                }
                catch (Exception ex)
                {
                    return null;
                }

                return new SelectList(listName, "Key", "Value");
            }
        }

        public  CustomersMappedList GetCustomerMappedlist(int listId,DataManager dataManager)
        {
            var list = new CustomersMappedList();
            list.CustomerMappedListDetails = new List<CustomerListDetails>();

            using (var context = new DataEntities())
            {
                var details = (from listMapeer in context.CustomerListMappings.Where(x => x.ListId == listId && x.IsActive)
                               join cust in context.Customers.Where(x => x.IsActive) on listMapeer.CustomerId equals cust.Id
                               select new CustomerListDetails
                               {
                                   ListID = listMapeer.ListId,
                                   CustomerId = cust.Id,
                                   Name = cust.Name,
                                   EmailId = cust.Email,
                                   DOB = cust.DateOfBirth,
                                   IsUnSubscribed = listMapeer.IsUnsubscribed,
                                   DateAdded = listMapeer.CreatedDate,
                                   LastUpdated = listMapeer.ModifiedDate
                               }).AsEnumerable();

                DataOperations operation = new DataOperations();
                if (dataManager.Sorted != null && dataManager.Sorted.Count > 0)
                {
                    details = (IEnumerable<CustomerListDetails>)operation.PerformSorting(details, dataManager.Sorted);
                }

                if (dataManager.Where != null && dataManager.Where.Count > 0)
                {
                    details = (IEnumerable<CustomerListDetails>)operation.PerformWhereFilter(details, dataManager.Where, dataManager.Where[0].Operator);
                }

                if (dataManager.Search != null && dataManager.Search.Count > 0)
                {
                    details = (IEnumerable<CustomerListDetails>)operation.PerformSearching(details, dataManager.Search);
                }

                this.TotalListCount = details.Count();
                if (dataManager.Skip != 0)
                {
                    details = (IEnumerable<CustomerListDetails>)operation.PerformSkip(details, dataManager.Skip);
                }

                if (dataManager.Take != 0)
                {
                    details = (IEnumerable<CustomerListDetails>)operation.PerformTake(details, dataManager.Take);
                }

                list.CustomerMappedListDetails = details.ToList();
            }
            return list;
        }

        public static bool DeleteUserFromList(int listId, int userId)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    var deleteUser = context.CustomerListMappings.Where(x => x.ListId == listId && x.CustomerId == userId).FirstOrDefault();
                    deleteUser.IsActive = false;
                    context.SaveChanges();

                }

            }
            catch(Exception)
            {
                return false;
            }
            return true;
            
        }
        /// <summary>
        /// To add group details
        /// </summary>
        /// <param name="groupName">groupName</param>
        /// <returns></returns>
        public static bool AddGroupDetails(string groupName)
        {
            bool isSuccess = false;
            try
            {
                using (var entities = new DataEntities())
                {
                    var isGroupNameAlreadyAvailable = (from grp in entities.CustomerLists where grp.Name == groupName select grp).Any();
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
