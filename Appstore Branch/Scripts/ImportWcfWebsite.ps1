$services = "ShoppingCartService:Com\AppStore\ShoppingCart\WcfHost",
"ContentService:Com\AppStore\Content\WcfHost",
"AuthenticationService:Com\AppStore\Authentication\WcfHost",
"DeliveryService:Com\AppStore\Delivery\WcfHost",
"CustomerService:Com\AppStore\Customer\WcfHost",
"LocationService:Com\AppStore\Location\WcfHost",
"OrderService:Com\AppStore\Order\WcfHost",
"PaymentService:Com\AppStore\Payment\WcfHost",
"FavouriteService:Com\AppStore\Favourites\WcfHost",
"ProductService:Com\AppStore\Product\WcfHost",
"LoyaltyService:Com\AppStore\Loyalty\WcfHost",
"StoreHouseService:Com\AppStore\StoreHouse\WcfHost" ,
"IntegrationService:Com\Integration\WcfHost",
"NotificationService:Com\AppStore\Notification\WcfHost",
"CouponService:Com\AppStore\Coupon\WcfHost",
"DeviceIdentificationService:Com\AppStore\DeviceIdentification\WcfHost",
"SmartStore:Com\AppStore\SmartStore\WcfHost",
"GroceryProductService:Com\AppStore\GroceryProduct\WcfHost",
"CoreProductService:Com\AppStore\CoreProduct\WcfHost", 
"PriceService:Com\AppStore\Price\WcfHost"


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
$notfound="Not Found Services are "
$found="Found Services are "

appcmd add APPPOOL /name:"AppStore" /startMode:AlwaysRunning 

appcmd set apppool /apppool.name: "AppStore" /managedRuntimeVersion:v4.0 

appcmd add APPPOOL /name:"Integration" /startMode:AlwaysRunning 
appcmd set apppool /apppool.name: "Integration" /managedRuntimeVersion:v4.0 

$appstoreHost = $a.tostring() + "\AppStoreHost"

[IO.Directory]::CreateDirectory($appstoreHost) 

appcmd add site /name:"AppStore" /id:2 /physicalPath:$appstoreHost /bindings:http://*:81 /applicationDefaults.applicationPool:"AppStore"


foreach($element in $services)
{
	$sarr=$element.split(":")
	$b = "\" + $sarr[1]
	$c = $a.tostring() + $b
	set-location $c
	$exists = Test-Path $c
	if ($exists)
	{
        $virtualDirectory=$sarr[0]
		appcmd add app /site.name:"AppStore" /path:/$virtualDirectory /physicalPath:$c
		$found+= " , " + $sarr[0]
	}
	else
	{
		$notfound+= " , " + $sarr[0]
		continue
	}
	
	Set-Location $a
}

IISRESET

$found
$notfound