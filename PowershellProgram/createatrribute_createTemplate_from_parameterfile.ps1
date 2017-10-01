try 
{
$WebProjectpath=$env:WebProjectpath
$Applicationparameterfile=$env:Applicationparameterfile
$cookbooklocation=$env:cookbooklocation

$webconfigfile=$cookbooklocation+"\templates\dev\Web.config.erb"

$GroceryWebfile=$cookbooklocation+"\templates\dev\GroceryWeb.tesco.config.erb"

$Applicationparameterfile=$Applicationparameterfile+"\Manifest_Delivery_UK_DEV_Application_Parameters_Subscriptions.xml"

$attributefile = $cookbooklocation+"\attributes\dev_configuration.rb"

Set-Content $attributefile "if node.chef_environment == 'dev'"

$cookbookName = "ukghs_poc"



#$Applicationparameterfile=[xml] (Get-Content $Applicationparameterfile)

$doc = new-object System.Xml.XmlDocument
$doc.Load($Applicationparameterfile)
Set-ItemProperty $WebProjectpath -name IsReadOnly -value $false
$keyword="Tesco.Com.Subscriptions.Grocery.sln"
$WebProjectpath = (Get-Content $WebProjectpath)
$matchpath=$WebProjectpath -match $keyword | select -last 1 
$fullpath=$matchpath  -replace "$keyword.*$","" | %{$_ -replace '"',""}
#$fullpath=[xml] (Get-Content $($fullpath+"\web\web.config"))
$fullpathGrocerywebfile=$fullpath+"\web\GroceryWeb.tesco.config"
$fullpath= $fullpath+"\web\web.config"

$config=new-object System.Xml.XmlDocument
$config.Load($fullpath)
$Grocerywebconfig=new-object System.Xml.XmlDocument
$Grocerywebconfig.Load($fullpathGrocerywebfile)

$nodelist=$doc.SelectNodes("/Application/Environment/Web.Config/AppSettings")

$nodelist1=$doc.SelectNodes("/Application/Environment/GroceryWeb.tesco.config/AppSettings")

$confignodelist=$config.SelectNodes("/configuration/appSettings")
$confignodelist1=$Grocerywebconfig.SelectNodes("/configuration/appSettings")

Add-Content $attributefile "#Web.config" 

foreach ($item in $nodelist.add) 
{
 foreach($configitem in $confignodelist.add)
 {
 
 if ($item.key -eq $configitem.key)

 {
 
 #$item.value
 
 #$configitem.Attributes['value'].Value =  $item.value

 if($item.value -match "@@@(.*)@@@")
 {
  $configitem.Attributes['value'].Value =  $item.value
 }
 else
 {
  $configitem.Attributes['value'].Value =  "###$cookbookName###$($item.key)###"
 Add-Content $attributefile "default['$cookbookName']['$($item.key)'] = ""$($item.value)""" 
 }



 }
 
 }

}
$config.save($webconfigfile)

###################################################--------GroceryWeb.tesco.config------################################

Add-Content $attributefile "#GroceryWeb.tesco.config" 

foreach ($item in $nodelist1.add) 
{
 foreach($configitem in $confignodelist1.add)
 {
 
 if ($item.key -eq $configitem.key)

 {
 
 #$item.value
 
 #$configitem.Attributes['value'].Value =  $item.value

  if($item.value -match "@@@(.*)@@@")
 {
  $configitem.Attributes['value'].Value =  $item.value
 }
 else
 {
  $configitem.Attributes['value'].Value =  "###$cookbookName###$($item.key)###"
 Add-Content $attributefile "default['$cookbookName']['$($item.key)'] = ""$($item.value)""" 
 }



 }
 
 }

}
$Grocerywebconfig.save($GroceryWebfile)


###########################################################################################


$config.save($webconfigfile)
$webconfigfile_content= gc $webconfigfile
#$webconfigfile | %{$_ -replace "&lt;","<" -replace "&gt;",">" } | Set-Content $webconfigfile
$webconfigfile_content | %{$_  -replace "@@@(.*)@@@",'<%= @${1} %>'} | Set-Variable webconfigfile_content
$webconfigfile_content | %{$_  -replace "###(.*)###(.*)###",'<%= node[''${1}''][''${2}''] %>'} | Set-Content $webconfigfile

###########################################################################################

$Grocerywebconfig.save($GroceryWebfile)
$Grocerywebconfigfile= gc $GroceryWebfile
$Grocerywebconfigfile | %{$_  -replace "###(.*)###(.*)###",'<%= node[''${1}''][''${2}''] %>'} | Set-Content $GroceryWebfile
Add-Content $attributefile "end"
}
catch
{
$Errormessage=$_.Exception.Message
throw $Errormessage 
}