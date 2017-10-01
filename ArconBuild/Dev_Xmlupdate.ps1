$RMpath=$env:ServerFile
#$RMpath="E:\POC\Servers.xml"
Set-ItemProperty $RMpath -name IsReadOnly -value $false
$xml = [xml](Get-Content $RMpath)
$version=$env:NOLIOARGUMENT
#$version="NotificationProvider_1.1.0.196"
$ComponentName=$env:COMPONENT
#$ComponentName="Notification"

Write-Host $RMpath
Write-Host $version
Write-Host $ComponentName
$currentdate=29/09/2015
$xml.REWARDSS.WEBSITE | % {
 if($_.COMPONENTNAME -eq $ComponentName) 
  {
  if($ComponentName -eq "RBC")
  {
  #$Oldvalue=$_.CE_DEV
   # $_.CE_DEVOLD=$Oldvalue -replace "^.*\.",""
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "RBC_" + $version
  #$Newvalue=$_.CE_DEV
  #$_.CE_DEVNEW=$Newvalue -replace "^.*\.",""
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  
  
  if($ComponentName -eq "RewardsCSC")
  {
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "RewardsCSC_" + $version
 
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "OnlineWebsite")
  {
  Write-Host $ComponentName
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "OnlineWeb_" + $version
  
 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
    if($ComponentName -eq "IntegrationService")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "IntegrationService_" + $version.split('_')[-1]

 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "Reprocessor")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "Reprocessor_" + $version.split('_')[-1]
$currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
    if($ComponentName -eq "OrderVoucherETLService")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "OrderVoucherETLService_" + $version.split('_')[-1]

 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }

  if($ComponentName -eq "AcornTokenService")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "AcornTokenService_" + $version.split('_')[-1]
 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "PostalResponse")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "PostalResponse_" + $version.split('_')[-1]
 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  #TAPI Components
  if($ComponentName -eq "Search")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "SearchProvider_" + $version.split('_')[-1]
$currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "Range")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "RangeProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   if($ComponentName -eq "Product")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "ProductProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "CustomerOrder")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "CustomerOrderProvider_" + $version.split('_')[-1]
 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   if($ComponentName -eq "StoredValue")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "StoredValueProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "Notification")
  {

  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "NotificationProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   #RAPI Components
   
  if($ComponentName -eq "RBS")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "RBSProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   if($ComponentName -eq "TradingPartner")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "TradingPartnerProvider_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   if($ComponentName -eq "RFS")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "RFS_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "CustomerProfile")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "CustomerProfile_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "ValidationStore")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "ValidationStore_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "TokenManagement")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "TokenManagement_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
   if($ComponentName -eq "TokenProcessor")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "TokenProcessor_" + $version.split('_')[-1]
$currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "Failover")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "Failover_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  #
  if($ComponentName -eq "Automiles")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "Automiles_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
  $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "Qpoller")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "Qpoller_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
  $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "ProductInduction")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "ProductInduction_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
  $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "PartnersFeed")
  {
  
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "PartnersFeed_" + $version.split('_')[-1]
 $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  if($ComponentName -eq "PartnerIntegration")
  {
 
  $_.CE_DEV="ReleaseManifest_Acorn_UK_" + "PartnerIntegration_" + $version.split('_')[-1]
  $currentdate=Get-Date -format "yyyyMMdd"
    $_.CE_DEV_DEPLOEDDATE=$currentdate.ToString()
  }
  }
  
 
 }
 
 
 
 $xml.Save($RMpath)
