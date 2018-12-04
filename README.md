# Umbraco Recycle Bin Manager

An API to allow the Umbraco content and media recycle bins to be managed. It is expected to be called via a scheduled task.

The `CleanRecycleBins` call will remove items in the Content and Media recycle bins that are older than the number of days specified in the following `web.config` setting:

	<add key="CleanRecycleBinsDaysOlderThan" value="30" />

Setting the value to 0 (zero) disables any deletions.

If there are large number of items to delete you may encounter timeouts:

- A timeout reported by PetaPoco in Umbraco's `ContentService.GetContentInRecycleBin()` method relates to the connection timeout in the database connection string, specified using the `umbracoDbDSN` setting in `web.config`.
- When running on Azure a 4 minute timeout [relates to the Azure load balancer](https://azure.microsoft.com/en-us/blog/new-configurable-idle-timeout-for-azure-load-balancer/).  

## Scheduled Job Script

The template script is `ExampleScheduledTask.ps1`.

Create a version for your environment by configuring the URL to the website / API call in the $URL variable, the API user and API key. 

If your Umbraco installation uses [Escc.BasicAuthentication.WebApi](https://github.com/east-sussex-county-council/Escc.BasicAuthentication.WebApi) these should match the credentials in `web.config` for the Umbraco project.
