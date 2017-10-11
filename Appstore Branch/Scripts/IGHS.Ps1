$sols = "Tesco.Com:Com",
"Tesco.Com.Web.Mvc:Com",
"YUICompressor:Com/Lib/YUICompressor/Compressor/YUICompressor",
"PaymentPlatform:Com/Enterprise/Cryptography",
"TokenisationService:Com/Enterprise/Cryptography",
"Endeca:Com/Endeca",
"Integration:Com/Integration",
"Integration.WcfHost:Com/Integration",
"BrandBank.App:Com/Integration",
"Enterprise.Common:Com/Enterprise",
"Location.Contracts:Com/AppStore/Location",
"Location.Library:Com/AppStore/Location",
"Location.WcfClient:Com/AppStore/Location",
"Location.WcfHost:Com/AppStore/Location",
"Tesco.Com.Appstore.Search:Com/AppStore/Search",
"Favourite.Service.Contract:Com/AppStore/Favourites",
"Favourite.Service.Library:Com/AppStore/Favourites",
"Favourite.Service.WcfClient:Com/AppStore/Favourites",
"Favourite.Service.WcfHost:Com/AppStore/Favourites",
"Order.Service.Contracts:Com/AppStore/Order",
"Order.Service.Library:Com/AppStore/Order",
"Order.Service.WcfClient:Com/AppStore/Order",
"Order.Service.WcfHost:Com/AppStore/Order",
"Order.Contract:Com/AppStore/Grocery/Business/Order",
"Order.Component:Com/AppStore/Grocery/Business/Order",
"Order.Component.WcfHost:Com/AppStore/Grocery/Business/Order",
"Payment.Service.Contracts:Com/AppStore/Payment",
"Payment.Service.Library:Com/AppStore/Payment",
"Payment.Service.WcfClient:Com/AppStore/Payment",
"Payment.Service.WcfHost:Com/AppStore/Payment",
"Product.Adapter:Com/AppStore/Adapters/Product",
"GroceryProduct.Wraper:Com/AppStore/Adapters/Product/GroceryProduct",
"Product.Wraper:Com/AppStore/Adapters/Product/Product",
"ShoppingCart.Service.Contract:Com/AppStore/ShoppingCart",
"ShoppingCart.Service.Library:Com/AppStore/ShoppingCart",
"ShoppingCart.Service.WcfClient:Com/AppStore/ShoppingCart",
"ShoppingCart.Service.WcfHost:Com/AppStore/ShoppingCart",
"Authentication.Contracts:Com/AppStore/Authentication",
"Authentication.Library:Com/AppStore/Authentication",
"Authentication.Client:Com/AppStore/Authentication",
"Authentication.WcfHost:Com/AppStore/Authentication",
"Loyalty.Service.Contracts:Com/AppStore/Loyalty",
"Loyalty.Service.Library:Com/AppStore/Loyalty",
"Loyalty.Service.WcfClient:Com/AppStore/Loyalty",
"Loyalty.Service.WcfHost:Com/AppStore/Loyalty",
"Coupon.Service.Contract:Com/AppStore/Coupon",
"Coupon.Service.Library:Com/AppStore/Coupon",
"Coupon.Service.WcfClient:Com/AppStore/Coupon",
"Coupon.Service.WcfHost:Com/AppStore/Coupon",
"WebAPIClientBase:Com/WebAPIClient/WebAPIClientBase",
"ProductListAPIClient:Com/WebAPIClient/ProductListAPIClient",
"TransportAPIClient:Com/WebAPIClient/TransportAPIClient",
"TescoLocationAPIClient:Com/WebAPIClient/TescoLocationAPIClient",
"DigitalContentAPIClient:Com/WebAPIClient/DigitalContentAPIClient",
"AppStore.Grocery.Business.OrderCheckout:Com/AppStore/Grocery/Business/OrderCheckout",
"AppStore.Grocery.Business.Product:Com/AppStore/Grocery/Business",
"AppStore.Grocery.Business.Content:Com/AppStore/Grocery/Business/Content",
"AppStore.Grocery.Business.Shopping:Com/AppStore/Grocery/Business/Shopping",
"AppStore.Grocery.Business.Customer:Com/AppStore/Grocery/Business/Customer",
"AppStore.Grocery.Business.Delivery:Com/AppStore/Grocery/Business/Delivery",
"Tesco.Com.Web.Common:Com/Web",
"Tesco.Com.Web.UI.Grocery.Common:Com/Web/UI/Grocery/Common",
"Tesco.Com.Web.UI.Grocery.Common.Resource:Com/Web/UI/Grocery/Common",
"Tesco.Com.Web.Utilities:Com/Web/Tools",
"Tesco.Com.Web.UIAssets:Com/Web/UIAssets",
"Tesco.Com.Web.UI.Grocery.Checkout:Com/Web/UI/Grocery/Checkout",
"Tesco.Com.Web.UI.Grocery.Delivery:Com/Web/UI/Grocery/Delivery",
"Tesco.Com.Web.UI.Grocery.MyAccount:Com/Web/UI/Grocery/MyAccount",
"Tesco.Com.Web.UI.Grocery.Products:Com/Web/UI/Grocery/Products",
"Tesco.Com.Web.UI.Grocery.ShoppingCart:Com/Web/UI/Grocery/ShoppingCart",
"Tesco.Com.Web.UI.Grocery.Home:Com/Web/UI/Grocery/Home",
"Tesco.Com.Web.UI.GroceryHost:Com/Web/UI/Grocery"

Set-ExecutionPolicy Unrestricted
Set-Location -Path .. -Passthru
$a = Get-Location;$a
#For setting the environment

$s = $env:Path.tostring()
$strs ="C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
$found = $s.Contains($strs)

if (!$found)
{
	$env:Path = "C:\Windows\Microsoft.NET\Framework64\v4.0.30319;" + $env:Path 
}
$notfound="Not Found Solutions are "
$found="Found Solutions are "
foreach($element in $sols)
{
	$sarr=$element.split(":")
	$b = "\" + $sarr[1]
	$c = $a.tostring() + $b
	set-location $c
	$sln = $sarr[0] + ".sln"
	$exists = Test-Path $sln
	if ($exists)
	{
		MSBuild $sln
		if($LastExitCode -ne 0)
		{	
			break;
		}
		$found+= " , " + $sln

	}
	else
	{
		$notfound+= " , " + $sln
		continue
	}
	
	Set-Location $a
}
$found
$notfound





