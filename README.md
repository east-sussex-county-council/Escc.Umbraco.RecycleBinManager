# Umbraco Recycle Bin Manager

An API to allow the Umbraco content and media recycle bins to be managed. It is expected to be called via a scheduled task.

The `CleanRecycleBins` call will remove items in the Content and Media recycle bins that are older than the number of days specified in the following `web.config` setting:

	<add key="CleanRecycleBinsDaysOlderThan" value="30" />

Setting the value to 0 (zero) disables any deletions.

## Scheduled Job Script

The template script is in `Escc.Umbraco.RecycleBinManager.ScheduledJob`.

Create a version for your environment by configuring the URL to the website / API call in the $URL variable, the API user and API key. These should match those in `web.config`, as described in the documentation for [Escc.BasicAuthentication.WebApi](https://github.com/east-sussex-county-council/Escc.BasicAuthentication.WebApi).
