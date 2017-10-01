#$WebProjectpath=$env:WebProjectpath
#$Applicationparameterfile=$env:Applicationparameterfile
#$devfile=$env:devfile
#$Applicationparameterfile=[xml] (Get-Content $Applicationparameterfile)

<#$WebProjectpath= "D:\MSBuildTask\Dev.Subscription_Website_Globaldev_Nexus_0.16.1006.1\logs\Tesco.Com.Subscriptions.Grocery.log"
$Applicationparameterfile="D:\Test\Manifest_Delivery_UK_DEV_Application_Parameters_Subscriptions.xml"
$devfile="D:\Test\DEV\web.cofig"

$Applicationparameterfile=[xml] (Get-Content $Applicationparameterfile)

$doc = new-object System.Xml.XmlDocument
$doc.Load($Applicationparameterfile)
Set-ItemProperty $WebProjectpath -name IsReadOnly -value $false
$keyword="Tesco.Com.Subscriptions.Grocery.sln"
$WebProjectpath = (Get-Content $WebProjectpath)
$matchpath=$WebProjectpath -match $keyword | select -last 1 
$fullpath=$matchpath  -replace "$keyword.*$","" | %{$_ -replace '"',""}
#$fullpath=[xml] (Get-Content $($fullpath+"\web\web.config"))
$fullpath= $fullpath+"\web\web.config"
$cofig=new-object System.Xml.XmlDocument
$cofig.Load($fullpath)

$nodelist=$doc.SelectNodes("/Application/Environment/Web.Config/AppSettings")
$confignodelist=$cofig.SelectNodes("/configuration/appSettings")

foreach ($item in $nodelist.add) 
{
 foreach($configitem in $confignodelist.add)
 {
 
 if ($item.key -eq $configitem.key)

 {
 
 #$item.value
 
 $configitem.Attributes['value'].Value =  $item.value

 }
 
 }

}
$cofig.save($devfile)#>


############################################above Script is orginal script###################################



#$WebProjectpath=$env:WebProjectpath
#$Applicationparameterfile=$env:Applicationparameterfile
#$devfile=$env:devfile
#$Applicationparameterfile=[xml] (Get-Content $Applicationparameterfile)

#$WebProjectpath= "D:\MSBuildTask\Dev.Subscription_Website_Globaldev_Nexus_0.16.1006.1\logs\Tesco.Com.Subscriptions.Grocery.log"
$Applicationparameterfile="D:\MSBuildTask\ApplicationPrameter\DEV\Manifest_Delivery_UK_DEV_Application_Parameters_Subscriptions.xml"
$devfile="D:\MSBuildTask\ApplicationPrameter\DEV"

$doc = new-object System.Xml.XmlDocument
$doc.Load($Applicationparameterfile)

#Set-ItemProperty $WebProjectpath -name IsReadOnly -value $false
#$keyword="Tesco.Com.Subscriptions.Grocery.sln"
#$WebProjectpath = (Get-Content $WebProjectpath)
#$matchpath=$WebProjectpath -match $keyword | select -last 1 
#$fullpath=$matchpath  -replace "$keyword.*$","" | %{$_ -replace '"',""}


#$fullpath=[xml] (Get-Content $($devfile+"\web\web.config"))

$fullpath= $devfile+"\web\web.config"
$cofig=new-object System.Xml.XmlDocument
$cofig.Load($fullpath)

$nodelist=$doc.SelectNodes("/Application/Environment/Web.Config/AppSettings")
$confignodelist=$cofig.SelectNodes("/configuration/appSettings")

foreach ($item in $nodelist.add) 
{
 foreach($configitem in $confignodelist.add)
 {
 
 if ($item.key -eq $configitem.key)

 {
 
 #$item.value
 
 $configitem.Attributes['value'].Value =  $item.value

 }
 
 }

}
$cofig.save($devfile)