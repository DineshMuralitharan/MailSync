using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailSync.Models;

namespace MailSync.Controllers
{
    public class CampaignController : Controller
    {
        // GET: Campaign
        public ActionResult Campaign()
        {
            return View();
        }

        /// <summary>
        /// Get campaign details
        /// </summary>
        public ActionResult CampaignDetails()
        {
            CampaignModel campDetails = new CampaignModel();

            campDetails.Values = campDetails.GetCampaignList();
            return View(campDetails);
        }

        public ActionResult CampaignWizard(int typeId)
        {
            var triggerList = new CampaignModel().GetCampaignDetailList(typeId);

            if (triggerList != null)
            {
                ViewBag.TriggerList = triggerList;
                ViewBag.TypeId = typeId;
                return View();
            }
            else
            {
                return RedirectToAction("CampaignDetails");
            }
        }

        /// <summary>
        /// save new campaign details
        /// </summary>
        public JsonResult AddNewCampaign(string campaignName, int campaignType, int customerListId)
        {
            var result = new CampaignModel().AddCampaign(campaignName, campaignType, customerListId);
            return this.Json(new { result, JsonRequestBehavior.AllowGet });
        }

        public ActionResult EmailTemplateDetails()
        {
            return View();
        }

        /// <summary>
        /// save Email details
        /// </summary>
        public JsonResult AddNewEmailDetails(string emailSubject, string name, int campaignId, string emailAddress)
        {
            var result = new CampaignModel().AddEmailDetails(emailSubject, name, campaignId, emailAddress);
            return this.Json(new { result, JsonRequestBehavior.AllowGet });
        }

        [Route("template/edit/{templateId:int}")]
        public ActionResult EditTemplate(int templateId)
        {
            TemplateModel template = new TemplateModel();
            var details =  template.GetTemplate(templateId);
            return View(details);
        }

        /// <summary>
        /// save Email details
        /// </summary>
        public JsonResult AddSchedule(bool isOnce, bool isEveryDay, int campaignId, string date)
        {
            var result = new CampaignModel().AddSchedule(isOnce, isEveryDay, campaignId, date);
            return this.Json(new { result, JsonRequestBehavior.AllowGet });
        }

        /// <summary>
        /// save Email details
        /// </summary>
        public JsonResult AddTrigger(int campaignTypeId)
        {
            var result = new CampaignModel().AddTrigger(campaignTypeId);
            return this.Json(new { result, JsonRequestBehavior.AllowGet });
        }
        public ActionResult ScheduleConfirmation()
        {
            return View();
        }

        [Route("campaign/wizard/{campignTypeId:int}/template/{camapignId:int}")]
        public ActionResult EditCampionTemplate(int campignTypeId, int camapignId)
        {
            ViewBag.CampignId = camapignId;
            ViewBag.campignTypeId = campignTypeId;
            ViewBag.TemplateDetails = TemplateModel.GetTemplateDetails();
            return View();
        }

        [Route("campaign/wizard/{campignTypeId:int}/template/{camapignId:int}/exiting/{templateId:int}")]
        public ActionResult EditExistingTemplate(int campignTypeId, int camapignId,int templateId)
        {
            ViewBag.CampignId = camapignId;
            ViewBag.campignTypeId = campignTypeId;
            TemplateModel template = new TemplateModel();
            var details = template.GetTemplate(templateId);
            return View(details);
        }
        
        [Route("camapign/updatetempplate/{campaignID:int}/{templatedName}")]
        public JsonResult UpdateTemplateCampiagn(int campaignID, string templatedName, string templateContent = null)
        {
            TemplateModel model = new TemplateModel();
            return this.Json(new { data = model.UpdateTemplateCapaign(campaignID, templatedName, templateContent) });
        }

        /// <summary>
        /// save Email details
        /// </summary>
        public ActionResult ScheduleCampaign()
        {
            var campaignIdList = Session["campaignIdList"] as List<int>;
            var result = new CampaignModel().ScheduleCampaign(campaignIdList);
            if(result)
            {
                return RedirectToAction("ScheduleConfirmation");
            }
            else
            {
                return RedirectToAction("CampaignDetails");
            }
        }
    }
}