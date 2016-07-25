# powershell.exe
# Set the Uri to suit the environment
# ===================================
# local:   https://hostname/umbraco/api/RecycleBinApi/CleanRecycleBins
# ===================================
$URL="https://hostname/umbraco/api/RecycleBinApi/CleanRecycleBins"
$user = "example"
$pass = "example"

$pair = "${user}:${pass}"
$bytes = [System.Text.Encoding]::ASCII.GetBytes($pair)
$base64 = [System.Convert]::ToBase64String($bytes)
$basicAuthValue = "Basic $base64"
$headers = @{ Authorization = $basicAuthValue }

$ProgressPreference = "SilentlyContinue"
Invoke-RestMethod -uri $URL -Headers $headers -TimeoutSec 1200

