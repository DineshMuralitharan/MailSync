using MailSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSync.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {
            CampaignModel campDetails = new CampaignModel();
            campDetails.Values = campDetails.GetCampaignList();
            return View(campDetails);
        }

        public ActionResult CampaignStatistics(int campaignId)
        {
            var campaignModel = new CampaignModel();
            ViewBag.CampaignDetails = campaignModel.GetCampaignDetailsByCampaignId(campaignId);

            ViewBag.EmailStatistics = campaignModel.GetEmailStatisticsByCampaignId(campaignId);

            return View();
        }
    }
}