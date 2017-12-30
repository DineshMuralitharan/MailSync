using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailSync.Data;

namespace MailSync.Models
{
    public class CampaignModel
    {
        public List<CampaignDetails> Values = new List<CampaignDetails>();

        /// <summary>
        /// Get all campaign list
        /// </summary>
        /// <returns>return all active campaign list</returns>
        public List<CampaignDetails> GetCampaignList()
        {
            List<CampaignDetails> campaignList = new List<CampaignDetails>();
            try
            {
                using (var entities = new DataEntities())
                {
                    campaignList = (from campaigns in entities.Campaigns
                                    join campaignType in entities.CampaignTypes on campaigns.Type equals campaignType.Id
                                    join campaignUser in entities.Users on campaigns.CreatedBy equals campaignUser.Id
                                    where campaigns.IsActive
                                    select new CampaignDetails
                                    {
                                        CampaignId = campaigns.Id,
                                        Name = campaigns.Name,
                                        Type = campaignType.Name,
                                        TypeId = campaignType.Id,
                                        CreatedByUser = campaignUser.Name,
                                        CreatedDate = campaigns.CreatedDate,
                                        OpenCount = (from emailStatistics in entities.EmailStatistics
                                                     where emailStatistics.CampaignId == campaigns.Id
                                                     && emailStatistics.IsActive && emailStatistics.IsOpen
                                                     select emailStatistics.IsOpen).Count(),
                                        MailStatus = (from email in entities.Emails
                                                      where email.CampaignId == campaigns.Id &&
                                                      email.IsMailSent
                                                      select email.Id).Any() ? "Sent" : (from email in entities.Emails
                                                                                         where email.CampaignId == campaigns.Id &&
                                                                                         email.IsStarted
                                                                                         select email.Id).Any() ? "Sending" : "Draft"

                                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignList;
        }

        /// <summary>
        /// Get all campaign list
        /// </summary>
        /// <returns>return all active campaign list</returns>
        /// 
        public List<CampaignDetails> GetCampaignDetailList(int campaignTypeId)
        {
            List<CampaignDetails> campaignList = new List<CampaignDetails>();
            try
            {
                using (var entities = new DataEntities())
                {
                    campaignList = (from campaign in entities.Campaigns
                                    where campaign.Type == campaignTypeId && campaign.IsActive && campaign.IsCompleted == false
                                    join type in entities.CampaignTypes on campaign.Type equals type.Id
                                    where type.IsActive
                                    join campList in entities.CampaignListMappings on campaign.Id equals campList.CampaignId
                                    where campList.IsActive
                                    join customerList in entities.CustomerLists on campList.ListId equals customerList.Id
                                    where customerList.IsActive
                                    join schedule in entities.ScheduleDetails on campaign.Id equals schedule.CampaignId into ScheduleTemp
                                    from interval in ScheduleTemp.Where(x => x.IsActive).DefaultIfEmpty()
                                    join detail in entities.EmailDetails on campaign.Id equals detail.CampaignsId into MailTemp
                                    from emailDetail in MailTemp.Where(x => x.IsActive).DefaultIfEmpty()
                                    join email in entities.Emails on campaign.Id equals email.CampaignId into EmailTemp
                                    from mail in EmailTemp.Where(x => x.IsActive).DefaultIfEmpty()
                                    select new CampaignDetails()
                                    {
                                        CampaignId = campaign.Id,
                                        Name = campaign.Name,
                                        Type = type.Name,
                                        IsDaily = interval != null ? interval.IsEveryDate : false,
                                        IsOnce = interval != null ? interval.IsOnce : false,
                                        IsMailDetailsAdded = emailDetail != null ? true : false,
                                        UserGroupName = customerList.Name,
                                        IsMailTemplatesAdded = mail != null ? mail.TemplateId != 5 ? true : false : false
                                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignList;
        }
        /// <summary>
        /// Get campaign details by campaign id
        /// </summary>
        /// <returns>return campaign details/returns>
        public CampaignDetails GetCampaignDetailsByCampaignId(int campaignId)
        {
            CampaignDetails campaignDetails = new CampaignDetails();
            try
            {
                using (var entities = new DataEntities())
                {
                    campaignDetails = (from campaigns in entities.Campaigns
                                       join mapping in entities.CampaignListMappings on campaigns.Id equals mapping.CampaignId into mappingDetails
                                       from map in mappingDetails.DefaultIfEmpty()
                                       join list in entities.CustomerLists on map.ListId equals list.Id into listDetails
                                       from listRecord in listDetails.DefaultIfEmpty()
                                       join campaignType in entities.CampaignTypes on campaigns.Type equals campaignType.Id
                                       join campaignUser in entities.Users on campaigns.CreatedBy equals campaignUser.Id
                                       where campaigns.Id == campaignId && campaigns.IsActive
                                       select new CampaignDetails
                                       {
                                           Name = campaigns.Name,
                                           Type = campaignType.Name,
                                           CreatedByUser = campaignUser.Name,
                                           ListId = map.ListId,
                                           ListName = listRecord.Name,
                                           CreatedDate = campaigns.CreatedDate,
                                           OpenCount = (from emailStatistics in entities.EmailStatistics
                                                        where emailStatistics.CampaignId == campaigns.Id
                                                        && emailStatistics.IsActive && emailStatistics.IsOpen
                                                        select emailStatistics.IsOpen).Count()
                                       }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignDetails;
        }

        /// <summary>
        /// Get campaign details by campaign id
        /// </summary>
        /// <returns>return campaign details/returns>
        public List<CampaignStatistics> GetLatestEmailStatistics()
        {
            List<CampaignStatistics> campaignDetails = new List<CampaignStatistics>();
            try
            {
                using (var entities = new DataEntities())
                {
                    campaignDetails = (from statistics in entities.EmailStatistics
                                       join customer in entities.Customers on statistics.CustomerId equals customer.Id
                                       join campaign in entities.Campaigns on statistics.CampaignId equals campaign.Id
                                       join campaignUser in entities.Users on campaign.CreatedBy equals campaignUser.Id
                                       where statistics.IsActive
                                       select new CampaignStatistics
                                       {
                                           CustomerName = customer.Name,
                                           OpenedDate = statistics.OpenedDate,
                                           Name = campaign.Name,
                                           CreatedByUser = campaignUser.Name,
                                           CreatedDate = campaign.CreatedDate,
                                       }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignDetails;
        }

        /// <summary>
        /// Get campaign details by campaign id
        /// </summary>
        /// <returns>return campaign details/returns>
        public List<CampaignStatistics> GetEmailStatisticsByCampaignId(int campaignId)
        {
            List<CampaignStatistics> campaignDetails = new List<CampaignStatistics>();
            try
            {
                using (var entities = new DataEntities())
                {
                    campaignDetails = (from statistics in entities.EmailStatistics
                                       join customer in entities.Customers on statistics.CustomerId equals customer.Id
                                       where statistics.CampaignId == campaignId && statistics.IsActive
                                       select new CampaignStatistics
                                       {
                                           CustomerName = customer.Name,
                                           OpenedDate = statistics.OpenedDate
                                       }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return campaignDetails;
        }

        public bool AddCampaign(string campaignName, int campaignType, int listId)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    var campaign = new Campaign()
                    {
                        Name = campaignName,
                        Type = campaignType,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsCompleted = false
                    };
                    context.Campaigns.Add(campaign);
                    var campaignMapping = new CampaignListMapping()
                    {
                        CampaignId = campaign.Id,
                        ListId = listId,
                        IsActive = true
                    };
                    context.CampaignListMappings.Add(campaignMapping);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ScheduleCampaign(List<int> campaignIdList)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    foreach (var list in campaignIdList)
                    {
                        var email = (from campaign in context.Emails
                                     where campaign.CampaignId == list && campaign.IsActive
                                     select campaign).FirstOrDefault();

                        email.IsStarted = true;
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddSchedule(bool isOnce, bool isEveryDay, int campaignId, string date)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    var customerListId = (from list in context.CampaignListMappings
                                          where list.CampaignId == campaignId && list.IsActive
                                          select list.ListId).FirstOrDefault();

                    var schedule = new ScheduleDetail()
                    {
                        IsOnce = isOnce,
                        IsEveryDate = isEveryDay,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsActive = true,
                        CampaignId = campaignId
                    };
                    context.ScheduleDetails.Add(schedule);

                    var email = new Email()
                    {
                        CustomerListId = customerListId,
                        CampaignId = campaignId,
                        TemplateId = 5, // Default Template
                        EMailTemplate = null,
                        CreatedDate = DateTime.Now,
                        ScheduledDate = DateTime.Now.AddHours(2),
                        IsMailSent = false,
                        IsActive = true,
                        Subject = "Default Template",
                        IsStarted = false
                    };
                    context.Emails.Add(email);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddTrigger(int campaignTypeId)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    var campaignName = (from camp in context.CampaignTypes
                                        where camp.Id == campaignTypeId && camp.IsActive
                                        select camp.Name).FirstOrDefault();

                    var campaignListId = (from camp in context.Campaigns
                                          where camp.Type == campaignTypeId && camp.IsActive
                                          join list in context.CampaignListMappings on camp.Id equals list.CampaignId
                                          where list.IsActive
                                          select list.ListId).FirstOrDefault();

                    var campaign = new Campaign()
                    {
                        Name = campaignName,
                        Type = campaignTypeId,
                        CreatedBy = 1,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsCompleted = false
                    };

                    context.Campaigns.Add(campaign);

                    var campaignMapping = new CampaignListMapping()
                    {
                        CampaignId = campaign.Id,
                        ListId = campaignListId,
                        IsActive = true
                    };

                    context.CampaignListMappings.Add(campaignMapping);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int AddEmailDetails(string emailSubject, string name, int campaignId, string emailAddress)
        {
            try
            {
                using (var context = new DataEntities())
                {
                    var emailinfo = new EmailDetail()
                    {
                        CampaignsId = campaignId,
                        emailSubject = emailSubject,
                        fromName = name,
                        fromemail = emailAddress,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };
                    context.EmailDetails.Add(emailinfo);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Campaign details
    /// </summary>
    public class CampaignDetails
    {
        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// Gets or sets a value of the Name.The member declare as public.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of the Type.The member declare as public.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value of the TypeId.The member declare as public.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets a value of the CreatedByUser.The member declare as public.
        /// </summary>
        public string CreatedByUser { get; set; }

        /// <summary>
        /// Gets or sets a value of the UserGroupName.The member declare as public.
        /// </summary>
        public string UserGroupName { get; set; }

        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public int OpenCount { get; set; }

        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public bool IsDaily { get; set; }

        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public bool IsOnce { get; set; }

        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public bool IsMailDetailsAdded { get; set; }

        /// <summary>
        /// Gets or sets a value of the OpenCount.The member declare as public.
        /// </summary>
        public bool IsMailTemplatesAdded { get; set; }

        /// <summary>
        /// Gets or sets a value of the CreatedDate.The member declare as public.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value of the MailStatus.The member declare as public.
        /// </summary>
        public string MailStatus { get; set; }

        /// <summary>
        /// Gets or sets a value of the ListId.The member declare as public.
        /// </summary>
        public int? ListId { get; set; }

        /// <summary>
        /// Gets or sets a value of the ListName.The member declare as public.
        /// </summary>
        public string ListName { get; set; }
    }

    /// <summary>
    /// Statistics details
    /// </summary>
    public class CampaignStatistics : CampaignDetails
    {
        /// <summary>
        /// Gets or sets a value of the OpenedDate.The member declare as public.
        /// </summary>
        public DateTime? OpenedDate { get; set; }

        /// <summary>
        /// Gets or sets a value of the CustomerName.The member declare as public.
        /// </summary>
        public string CustomerName { get; set; }
    }

    public enum CampaignType
    {
        BlogNotification = 1,

        AbondonedCartAlert = 2,

        EBookNotification = 3,

        RenewalAutomation = 4,

        WelcomeEMail = 5,

        Birthdaywish = 6,

        Custom = 7
    }
}