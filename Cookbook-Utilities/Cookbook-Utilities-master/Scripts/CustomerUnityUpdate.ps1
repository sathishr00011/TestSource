$Projectconfigpath=$env:Projectconfigpath
$configpath=($Projectconfigpath+"\ce\Unity_ce.config"),($Projectconfigpath+"\ap\Unity_ap.config")

$Applicationparameterfile=$env:Appfile
$cookbooklocation=$env:cookbook

foreach($configProjectpath in $configpath)
{

$configfile_ce=$cookbooklocation+"\templates\dev\ce\Unity.config.erb"
$configfile_ap=$cookbooklocation+"\templates\dev\ap\Unity.config.erb"

$Applicationparameterfiles= ($Applicationparameterfile+"\NonRestricted\DEV\CE\23.0\Manifest_IGHS_UK_CE_DEV_Application_Parameters.xml"),
           ($Applicationparameterfile+"\NonRestricted\DEV\AP\23.0\Manifest_IGHS_UK_AP_DEV_Application_Parameters.xml")


foreach($Applicationparameterfile in $Applicationparameterfiles)
{

$doc = new-object System.Xml.XmlDocument
$doc.Load($Applicationparameterfile)
Set-ItemProperty $configProjectpath -name IsReadOnly -value $false

##
$config=new-object System.Xml.XmlDocument
$config.Load($configProjectpath)
##

$doc.Application.Environment.Services.Service | % {
if ($_.name -eq "AppStore.Customer.Service")
{
$_."Unity.Config".WhiteListDirectoryFile | % {
$name = $_.Name
$value=$_.value

$config.unity.container.register | % {
if ($_.name -eq $name)
{
$_.constructor.param | % {
if ($_.name -eq "directoryFileName"  )
{
$_.value=$value
}
}
}
}
}
}
}

$config.save($configProjectpath)

if($Applicationparameterfile -match "Manifest_IGHS_UK_CE_DEV_Application_Parameters.xml" -and $configProjectpath -match "Unity_ce.config")
{ 
    copy-Item $configProjectpath -Destination "$configfile_ce" -Force
}
elseif($Applicationparameterfile -match "Manifest_IGHS_UK_AP_DEV_Application_Parameters.xml" -and $configProjectpath -match "Unity_ap.config")
{ 
    copy-Item $configProjectpath -Destination "$configfile_ap" -Force
}

}
}