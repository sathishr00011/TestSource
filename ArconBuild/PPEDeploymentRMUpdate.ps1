$RMpath=$env:ServerFile
#$RMpath="D:\CICD\Servers.xml"
$StgRootRM=$env:PPERootRMfile
#$StgRootRM="\\dotcom.tesco.org\shares\rewards\build_output\Acorn_Manifest\ManifestAutomation\PPE\PPERootRMFile_Rewards.xml"
#$ComponentName="CustomerProfile"
$ComponentName=$env:Component

Set-ItemProperty $RMpath -name IsReadOnly -value $false
$xml = [xml](Get-Content $RMpath)
$RootRMXML = [xml] (Get-Content $StgRootRM)

$xml.REWARDSS.WEBSITE | % {
$xmlcomponent=$_.COMPONENTNAME
$xmlcomponenttype=$_.COMPONENTTYPE
$Componentxlversion=$_.CE_DEV
$Componentxlversion=$Componentxlversion.split('_')[-1]
$RootRMXML.ReleaseManifest.Product.Services.Service.Component | % {
$ComponentmanifestRootRM=$_.ComponentManifest.split('_')[-1]
$ComponentmanifestRootRM=$ComponentmanifestRootRM.Substring(0,$ComponentmanifestRootRM.LastIndexOf("."))

if($xmlcomponenttype -eq "TAPI")
{
if ($_.ComponentName -eq $xmlcomponent)
{
if($ComponentmanifestRootRM -ne $Componentxlversion)
{
if($_.ComponentName -eq $ComponentName)
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_"+$xmlcomponent+"Provider_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"

}

}
}
}

}


#RAPI Components
$RootRMXML.ReleaseManifest.Product.Services.Service.Component | % {
$ComponentmanifestRootRM=$_.ComponentManifest.split('_')[-1]
$ComponentmanifestRootRM=$ComponentmanifestRootRM.Substring(0,$ComponentmanifestRootRM.LastIndexOf("."))
if($xmlcomponenttype -eq "RAPI")
{
if ($_.ComponentName -eq $xmlcomponent)
{
if($ComponentmanifestRootRM -ne $Componentxlversion)
{
if($_.ComponentName -eq $ComponentName)
{
if($ComponentName -eq "RBS")
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_"+$xmlcomponent+"Provider_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"
}
if($ComponentName -ne "RBS")
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_"+$xmlcomponent+"_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"
}
}

}
}
}
}

# Windows Services
$RootRMXML.ReleaseManifest.Product.Services.Service.Component | % {
$ComponentmanifestRootRM=$_.ComponentManifest.split('_')[-1]
$ComponentmanifestRootRM=$ComponentmanifestRootRM.Substring(0,$ComponentmanifestRootRM.LastIndexOf("."))

if(($xmlcomponenttype -eq "WINSERVICE") -Or ($xmlcomponenttype -eq "ETL"))
{
if ($_.ComponentName -eq $xmlcomponent)
{
if($ComponentmanifestRootRM -ne $Componentxlversion)
{
if($_.ComponentName -eq $ComponentName)
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_"+$xmlcomponent+"_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"

}

}
}
}

}

#Web Components
$RootRMXML.ReleaseManifest.Product.Services.Service.Component | % {
$ComponentmanifestRootRM=$_.ComponentManifest.split('_')[-1]
$ComponentmanifestRootRM=$ComponentmanifestRootRM.Substring(0,$ComponentmanifestRootRM.LastIndexOf("."))
if($xmlcomponenttype -eq "WEBSITE")
{
if ($_.ComponentName -eq $xmlcomponent)
{
if($ComponentmanifestRootRM -ne $Componentxlversion)
{
if($_.ComponentName -eq $ComponentName)
{
if($ComponentName -eq "OnlineWebsite")
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_OnlineWeb_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"
}
if($ComponentName -ne "OnlineWebsite")
{
$_.ComponentManifest="ReleaseManifest_Acorn_UK_"+$xmlcomponent+"_"+$Componentxlversion+".xml"
$_.State="ToBeInstalled"
}
}

}
}
}
}
}
$RootRMXML.Save($StgRootRM)
