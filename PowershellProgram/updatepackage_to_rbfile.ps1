$setups = ("Subscription.Website.Setup", "Subscription.IISWebsite.Setup")
#$packagename="Dev.Subscription_Website_Globaldev_Nexus_0.16.0617.4"
$packagename=$env:Package_Name
$version=$packagename.split('_')[-1]
$filepath=$env:Attributes
$file = gc $filepath
foreach($setup in $setups)
{
 $file | % {
if($_  -match $setup)
 {
  if($_ -match 'download_path')
  {
   $_ -replace "D:/Deployments.*zip","D:/Deployments/$setup.$version.zip"
  } 
  elseif($_ -match 'nexus_file_url')
  {
   $_ -replace "https.*zip","https://nexus.global.tesco.org/nexus/content/repositories/91_UKDeliverySaver/Dev/Dev.Subscription_Website_Globaldev_Nexus/$version/$setup.$version.zip"
  }
  elseif($_ -match 'msi_path')
  {
   $_ -replace "D:/Deployments.*msi","D:/Deployments/$setup.$version.msi"  
  }
}
else
 { $_ }
} | Set-Variable file
} 
$file | Set-Content $filepath