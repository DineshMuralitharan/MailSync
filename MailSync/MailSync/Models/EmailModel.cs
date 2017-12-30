using MailSync.Data; 
using Quartz;
using Syncfusion.EmailSender.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    /// <summary>
    /// create class for email model
    /// </summary>
    public class EmailModel : IJob
    { 
        /// <summary>
        /// To execute the scheduled campaign
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            // Process emails to send  
            this.ProcessSchedule();
        } 

        /// <summary>
        /// To process the scheduled campaign
        /// </summary>
        public void ProcessSchedule()
        {
            try
            {
                using (var entities = new DataEntities())
                {
                    var schduledCampaigns = (from campaigns in entities.Emails where (campaigns.ScheduledDate == DateTime.Now || campaigns.ScheduledDate <= DateTime.Now) && campaigns.IsActive == true && campaigns.IsMailSent == false select campaigns).ToList();
                    foreach(var campaign in schduledCampaigns)
                    {
                        var customerList = this.GetCustomerList(campaign.CustomerListId);
                        string emailAddress = string.Join(";", customerList.ToArray());
                        string emailSubject = (from emails in entities.Emails
                                       where emails.Id == campaign.Id
                                       join emailDetails in entities.EmailDetails on emails.CampaignId equals emailDetails.CampaignsId
                                       select emailDetails.emailSubject).FirstOrDefault();
                        bool isMailSent = MailSender.SendMessage(emailAddress, emailSubject, campaign.EMailTemplate);
                        Email email = (from emails in entities.Emails
                                       where emails.Id == campaign.Id 
                                       select emails).FirstOrDefault();
                        email.IsMailSent = isMailSent;
                        entities.Configuration.ValidateOnSaveEnabled = false;
                        entities.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }


        /// <summary>
        /// To return customer list based customerListId
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public List<string> GetCustomerList(int? listId)
        {
            List<string> customerList = new List<string>();
            try
            {
                using (var entities = new DataEntities())
                {
                    customerList = (from customerListMapping in entities.CustomerListMappings
                                    where customerListMapping.ListId == listId
                                    join customers in entities.Customers on customerListMapping.CustomerId equals customers.Id
                                    where customers.IsActive == true && customerListMapping.IsActive == true
                                    select customers.Email).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return customerList;
        }

    }
}