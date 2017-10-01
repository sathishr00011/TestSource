
#$ServerRMpath="D:\Serverxml\PPE and Live\Servers.xml"
Param
(
$ServerRMpath="",

#$DeploymentRMpath="D:\Serverxml\PPE and Live\DeploymentVersion_PPE_LIVE.xml"
$DeploymentRMpath=""
)

Set-ItemProperty $ServerRMpath -name IsReadOnly -value $false
$Sxml=[xml](Get-Content $ServerRMpath)
$Dxml = [xml](Get-Content $DeploymentRMpath)

$currentdate=29/09/2015

$Dxml.DeploymentVersion.PPE.Component | % {
$ComponentName = $_.Name
$ReleaseManifest = $_.ReleaseManifest
$Sxml.REWARDSS.WEBSITE | % {
 if($_.COMPONENTNAME -eq $ComponentName)
 {
  if($_.CE_PPE -ne $ReleaseManifest)
  {
   $_.CE_PPE = $ReleaseManifest
   $currentdate=Get-Date -format "yyyyMMdd"
   $_.CE_PPE_DEPLOEDDATE=$currentdate.ToString()
  }
 }
}
$Sxml.Save($ServerRMpath)
}

$Dxml.DeploymentVersion.LIVE.Component | % {
$ComponentName = $_.Name
$ReleaseManifest = $_.ReleaseManifest
$Sxml.REWARDSS.WEBSITE | % {
 if($_.COMPONENTNAME -eq $ComponentName)
 {
  if($_.CE_LIVE -ne $ReleaseManifest)
  {
   $_.CE_LIVE = $ReleaseManifest
   $currentdate=Get-Date -format "yyyyMMdd"
   $_.CE_LIVE_DEPLOEDDATE=$currentdate.ToString()
  }
 }
}
$Sxml.Save($ServerRMpath)
}

