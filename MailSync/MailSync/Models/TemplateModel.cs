using MailSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSync.Models
{
    public class TemplateModel
    {


        DataEntities context = new DataEntities();
        public static List<Template> GetTemplateDetails()
        {
            try
            {
                using (var entities = new DataEntities())
                {
                    var templateList = (from templates in entities.Templates where templates.IsActive select templates).ToList();
                    return templateList;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Template GetTemplate(int templateId)
        {
            return context.Templates.Where(x => x.Id == templateId).FirstOrDefault();
        }

        public bool UpdateTemplateCapaign(int campiagnId, string templateName, string templateContent)
        {
            try
            {
                using (var context = new DataEntities())
                {

                    var templatecon = context.Templates.FirstOrDefault();
                   var template = new Template(){
                       Name = templateName,
                       IsActive = true,
                       CreatedDate = DateTime.Now,
                       TemplateContent = templatecon.TemplateContent
                   };
                    context.Templates.Add(template);

                    var campaign = context.Emails.Where(x => x.CampaignId == campiagnId).FirstOrDefault();
                    campaign.TemplateId = template.Id;

                    context.SaveChanges();
                    
                }
            }
            catch(Exception ex)
            {
                return false;
            }            

            return true;
                
        }
    }



}