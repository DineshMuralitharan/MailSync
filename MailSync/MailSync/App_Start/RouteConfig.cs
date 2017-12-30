using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MailSync
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
             name: "Schedule Trigger",
             url: "campaign/schedule",
             defaults: new { controller = "Campaign", action = "ScheduleCampaign" }

         );
            routes.MapRoute(
              name: "Add Trigger",
              url: "campaign/addTrigger",
              defaults: new { controller = "Campaign", action = "AddTrigger" }
          );

            routes.MapRoute(
              name: "Add Schedule",
              url: "campaign/addSchedule",
              defaults: new { controller = "Campaign", action = "AddSchedule" }
          );

            routes.MapRoute(
               name: "Campaign Wizard",
               url: "campaign/wizard/{typeId}",
               defaults: new { controller = "Campaign", action = "CampaignWizard" }
           );

            routes.MapRoute(
               name: "Statistics",
               url: "statistics/overview",
               defaults: new { controller = "Statistics", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "Campaign Statistics",
             url: "statistics/campaign/{campaignId}",
             defaults: new { controller = "Statistics", action = "CampaignStatistics" }
         );
            routes.MapRoute(
                name: "EmailDetails",
                url: "emaildetails",
                defaults: new { controller = "Campaign", action = "EmailTemplateDetails", id = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "AddCampaign",
             url: "campaign/add",
             defaults: new { controller = "Campaign", action = "AddNewCampaign" }
         );
            routes.MapRoute(
            name: "AddEmailDetails",
            url: "campaign/addemail",
            defaults: new { controller = "Campaign", action = "AddNewEmailDetails" }
        );
            routes.MapRoute(
                name: "LogOffCallBack",
                url: "Account/LogOffCallBack",
                defaults: new { controller = "Account", action = "LogOffCallback" }
            );

            routes.MapRoute(
                name: "Confirmation",
                url: "campaign/confirmation",
                defaults: new { controller = "Campaign", action = "ScheduleConfirmation", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LogOnUsingAzure",
                url: "account/logonusingazure",
                defaults: new { controller = "Account", action = "LogOnUsingAzure", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Add Customer",
            url: "customer/addcustomerdetails",
            defaults: new { controller = "API", action = "AddCustomers" }
            );
            routes.MapRoute(
            name: "Add Group",
            url: "group/addgroupdetails",
            defaults: new { controller = "API", action = "AddGroup" }
            );
            routes.MapRoute(
            name: "Delete Customer",
            url: "customer/deletecustomer",
            defaults: new { controller = "API", action = "DeleteCustomers" }
            );
            routes.MapRoute(
            name: "Delete Customer List Mapping",
            url: "customer/deletecustomermapping",
            defaults: new { controller = "API", action = "DeleteCustomersListMapping" }
            );
            routes.MapRoute(
            name: "Add Cart",
            url: "customer/addcartdetails",
            defaults: new { controller = "API", action = "AddCardDetails" }
            );
            routes.MapRoute(
            name: "Unsubscribe List",
            url: "customer/unsubscribecustomerfromlist/{email}/{listId}",
            defaults: new { controller = "API", action = "UnSubscribeCustomersFromListMapping" }
            );
            routes.MapRoute(
            name: "UnSubscribe All",
            url: "customer/unsubscribeall/{email}",
            defaults: new { controller = "API", action = "UnSubcribeAll" }
            );
            routes.MapRoute(
                name: "campaigns",
                url: "campaigns",
                defaults: new { controller = "Campaign", action = "CampaignDetails", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Group",
                url: "group",
                defaults: new { controller = "List", action = "Group", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Documentation",
                url: "documentation",
                defaults: new { controller = "Home", action = "Documentation", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
