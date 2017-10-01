using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manifest.Contracts
{
    public enum ManifestType
    {
        AppStore,
        CouponService,
        NotificationService,
        LocationService,
        CustomerService,
        PaymentService,
        OrderService,
        Tibco,
        OMS,
        InStore,
        MyAccount,
        ProductIntegration,
        MobileDispatcher,
        BackOffice,
        Login,
        LoginUIAssets,
        BasketBuilding,
        FindProducts,
        CustomerProfile,
        Delivery,
       OrderCheckout,
        GroceryHost,
        Home,
        WindowsService,
        ConsoleApp,
        ReportingWebSite,
        DeliveryService,
        ShoppingCartService,
        AuthenticationService,
        FavouriteService,
        LoyaltyService,
        DeviceIdentificationService,
        OrderBusinessService,
        StoreHouseService,
        ContentService,
        EntprseAuthenticationService,
        UIAssets,
        ReportingManagementService
        

    }

    public enum ManifestArguments
    {
        Version,
        Regions,
        Template,
        Output,
        Tag,
        SearchDirectoryPath
    }
}
