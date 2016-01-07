using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.WebApi;

namespace Escc.Umbraco.RecycleBinManager.Controllers
{
    [Authorize]
    public class RecycleBinApiController : UmbracoApiController
    {
        private int _noOfDaysOlderThan;
        // UmbracoApiController exposes:
        // ApplicationContext ApplicationContext { get; }
        // ServiceContext Services { get; }
        // DatabaseContext DatabaseContext { get; }
        // UmbracoHelper Umbraco { get; }
        // UmbracoContext UmbracoContext { get; }

        //private readonly IContentService _contentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noOfDaysOlderThan">
        /// 
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet]
        [HttpPost]
        public HttpResponseMessage CleanRecycleBins(int noOfDaysOlderThan)
        {
            try
            {
                _noOfDaysOlderThan = noOfDaysOlderThan;

                // Check that the correct credentials have been supplied
                //var content = Request.Content.ReadAsStringAsync().Result;
                //if (!Authentication.AuthenticateUser(content))
                //{
                //    return Request.CreateResponse(HttpStatusCode.Forbidden);
                //}

                // OK, carry on
                return DeleteOldRecycleBinContent();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete items in Content and Media recycle bins that are older than the number of days
        /// defined in web.config.
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage DeleteOldRecycleBinContent()
        {
            var triggerDate = DateTime.Now.AddDays(_noOfDaysOlderThan * -1);

            // Content section Recycle Bin
            var contentNodes = Services.ContentService.GetContentInRecycleBin().Where(n => n.UpdateDate > triggerDate);

            foreach (var node in contentNodes)
            {
                //Services.ContentService.Delete(node);
            }

            // Media section Recycle Bin
            var mediaNodes = Services.MediaService.GetMediaInRecycleBin().Where(n => n.UpdateDate > triggerDate);

            foreach (var node in mediaNodes)
            {
                //Services.MediaService.Delete(node);
            }

            return null;
        }
    }
}