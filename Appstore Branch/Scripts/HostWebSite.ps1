Set-ExecutionPolicy Unrestricted
Set-Location -Path .. -Passthru
$a = Get-Location;$a

# For setting the environment

$s = $env:Path.tostring()
$strs ="c:\windows\system32\inetsrv\"
$found = $s.Contains($strs)

if (!$found)
{
	$env:Path = "c:\windows\system32\inetsrv\;" + $env:Path 
}

iisreset -stop

appcmd add APPPOOL /name:"Grocery" /startMode:AlwaysRunning 

appcmd set apppool /apppool.name:"Grocery" /managedRuntimeVersion:v4.0 

$groceryPath = $a.tostring() +"\Com\Web\UI\Grocery\GroceryHost"
$ReportingPath = $a.tostring() +"\Com\Web\UI\Reporting\Web.Reporting"


$JSPath = $a.tostring() +"\Com\Web\UIAssets\Web.UIAssets\UIAssets\js"
$ReportingJSPath = $a.tostring() +"\Com\Web\UIAssets\ReportingUIAssets\js"


$GroceryUIAssetsPath = $a.tostring() +"\Com\Web\UIAssets\Web.UIAssets\UIAssets"
$YUIPath = "D:\YUI\YUI\"
$ReportingUIAssetsPath = $a.tostring() +"\Com\Web\UIAssets\ReportingUIAssets\GB\reporting"


appcmd add site /name:"Grocery"  /physicalPath:$groceryPath /bindings:"http/*:80:Nakup.itesco.cz,https/*:443:Zabezpeceni.itesco.cz,http/*:80:Zabezpeceni.itesco.cz,http/*:80:ezakupy.tesco.pl,https/*:443:s.tesco.pl,http/*:80:s.tesco.pl,http/*:80:potravinydomov.itesco.sk,https/*:443:s.itesco.sk,http/*:80:s.itesco.sk,http/*:80:shoponline.tescolotus.com,https/*:443:s.tescolotus.com,http/*:80:s.tescolotus.com,http/*:80:eshop.tesco.com.my,https/*:443:s.tesco.com.my,http/*:80:s.tesco.com.my,http/*:80:bevasarlas.tesco.hu,https/*:443:s.tesco.hu,http/*:80:s.tesco.hu,http/*:80:js.ce-tescoassets.com,http/*:80:js.ap-tescoassets.com,http/*:80:assets.ce-tescoassets.com,http/*:80:assets.ap-tescoassets.com,http/*:80:pi.ce-tescoassets.com,http/*:80:pi.ap-tescoassets.com,https/*:443:secure.ce-tescoassets.com,https/*:443:secure.ap-tescoassets.com,http/*:80:elegou.cn.tesco.com,https/*:443:s.cn.tesco.com,http/*:80:pi.cn-tescoassets.com,http/*:80:js.cn-tescoassets.com,http/*:80:assets.cn-tescoassets.com,https/*:443:secure.cn-tescoassets.com,http/*:80:kapimda.kipa.com.tr,https/*:443:s.kipa.com.tr,http/*:80:api.Zabezpeceni.itesco.cz,http/*:80:api.s.tesco.pl,http/*:80:api.s.itesco.sk,http/*:80:api.s.tescolotus.com,http/*:80:api.s.tesco.com.my,http/*:80:api.s.tesco.hu,http/*:80:api.s.kipa.com.tr,http/*:80:api.s.cn.tesco.com" /applicationDefaults.applicationPool:"Grocery"
appcmd add site /name:"Reporting.ITesco"  /physicalPath:$reportingPath /bindings:http/*:80:ighs-reporting.dotcom.tesco.org /applicationDefaults.applicationPool:"Grocery"

appcmd add vdir /app.name:"Grocery/" /path:/UIAssets /physicalPath:$GroceryUIAssetsPath
appcmd add vdir /app.name:"Grocery/" /path:/js /physicalPath:$JSPath
appcmd add vdir /app.name:"Grocery/" /path:/yui /physicalPath:$YUIPath
appcmd add vdir /app.name:"Reporting.ITesco/" /path:/js /physicalPath:$ReportingJSPath
appcmd add vdir /app.name:"Reporting.ITesco/" /path:/UIAssets /physicalPath:$ReportingUIAssetsPath

iisreset
