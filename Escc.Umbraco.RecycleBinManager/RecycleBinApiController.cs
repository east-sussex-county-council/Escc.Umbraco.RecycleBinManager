using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Exceptionless;
using Umbraco.Core.Logging;
using Umbraco.Web.WebApi;

namespace Escc.Umbraco.RecycleBinManager
{
    [Authorize]
    public class RecycleBinApiController : UmbracoApiController
    {
        private readonly int _noOfDaysOlderThan;

        public RecycleBinApiController()
        {
            // Read number of days value from web.config
            var daysOlderThanStr = ConfigurationManager.AppSettings["CleanRecycleBinsDaysOlderThan"];

            // convert to an Integer, default to 0 on error.
            int.TryParse(daysOlderThanStr, out _noOfDaysOlderThan);
        }

        [HttpGet]
        [HttpPost]
        public HttpResponseMessage CleanRecycleBins()
        {
            try
            {
                return DeleteOldRecycleBinContent(_noOfDaysOlderThan);
            }
            catch (Exception ex)
            {
                LogHelper.Error(GetType(), ex.Message, ex);
                ex.ToExceptionless().Submit();
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Delete items in Content and Media recycle bins that are older than the number of days passed in
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage DeleteOldRecycleBinContent(int noOfDaysOlderThan)
        {
            // a value of 0 (zero) means "do nothing"
            if (noOfDaysOlderThan <= 0) return Request.CreateResponse(HttpStatusCode.OK);

            var triggerDate = DateTime.Now.AddDays(noOfDaysOlderThan * -1);

            // Content section Recycle Bin
            var contentNodes = Services.ContentService.GetContentInRecycleBin().Where(n => n.UpdateDate <= triggerDate);

            foreach (var node in contentNodes)
            {
                Services.ContentService.Delete(node);
            }

            // Media section Recycle Bin
            var mediaNodes = Services.MediaService.GetMediaInRecycleBin().Where(n => n.UpdateDate <= triggerDate);

            foreach (var node in mediaNodes)
            {
                Services.MediaService.Delete(node);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}