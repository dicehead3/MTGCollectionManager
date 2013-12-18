﻿using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Infrastructure.FlashMessage;

namespace Infrastructure.HtmlHelpers
{
    /// <summary>
    /// Flashmessage helper based on twitter bootstrap alerts
    /// </summary>
    public static class FlashMessageHelper
    {
        public static IHtmlString FlashMessage(this HtmlHelper helper, string id)
        {
            // Return empty string when no flashmessages were found in either viewdata or tempdata
            if (helper.ViewContext.TempData["flashMessage"] == null && helper.ViewContext.ViewData["flashMessage"] == null)
            {
                return new HtmlString(string.Format("<div id='{0}'></div>", id));
            }

            // Get flashmessages from tempdata else from viewdata
            var flashMessages = (List<FlashMessage.FlashMessage>)helper.ViewContext.TempData["flashMessage"] 
                                    ?? (List<FlashMessage.FlashMessage>)helper.ViewContext.ViewData["flashMessage"];

            // Create div tag, used as container for all flashmessages
            var container = new TagBuilder("div");
            
            // Set container id
            container.Attributes.Add("id", id);
            
            // Add flashmessages to container
            foreach(var flashMessage in flashMessages)
            {
                string cssClass;

                // Determine class name based on message type
                switch (flashMessage.Type)
                {
                    case FlashMessageType.Info:
                        cssClass = "alert alert-info alert-block";
                        break;
                    case FlashMessageType.Success:
                        cssClass = "alert alert-success alert-block";
                        break;
                    case FlashMessageType.Warning:
                        cssClass = "alert alert-block";
                        break;
                    case FlashMessageType.Error:
                        cssClass = "alert alert-error alert-block";
                        break;
                    default:
                        cssClass = "message-info";
                        break;
                }
                
                const string flashMessageTemplate = @"
                    <div class=""{0}""  onclick='$(this).fadeOut()'>
                        <h4>{1}</h4>
                        {2}
                    </div>                     
                ";
                
                // Add flashmessage to container
                container.InnerHtml += string.Format(flashMessageTemplate,
                    cssClass,
                    flashMessage.Title,
                    flashMessage.Message
                );
            }

            return new HtmlString(container.ToString());
        }
    }
}