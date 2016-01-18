# powershell.exe
# Set the Uri to suit the environment
# ===================================
# local:   https://hostname/umbraco/api/RecycleBinApi/CleanRecycleBins
# ===================================
$URL="https://hostname/umbraco/api/RecycleBinApi/CleanRecycleBins"

$NVC = New-Object System.Collections.Specialized.NameValueCollection
$NVC.Add('apiuser', 'example');
$NVC.Add('apikey', 'example');

$WC = New-Object System.Net.WebClient
$WC.UseDefaultCredentials = $true
$Result = $WC.UploadValues($URL,"post", $NVC);

[System.Text.Encoding]::UTF8.GetString($Result)
$WC.Dispose();

