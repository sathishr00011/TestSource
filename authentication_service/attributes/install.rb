#
# Authors:: Sathishkumar R & Vinoth M
# Cookbook Name:: authentication_service
# Attribute:: install
#
# Copyright (c) 2016, Tesco, All Rights Reserved.

# deployment parameters
default['authentication_service']['deployment_folder']='D:/Tesco/AppStore/AuthenticationService'
default['authentication_service']['components'] = ['AppStore.Authentication.Service.Contracts', 'AppStore.Authentication.Service.WCFHost','AppStore.Authentication.Service.Library','AppStore.Common.Library','Tesco.Com.Core']
default['authentication_service']['source_folder'] = 'D:/Tesco.Com/'
default['authentication_service']['install_location']='D:/Deployments/'
default['authentication_service']['artifacts']=[
  {
    'download_path' => 'D:/Deployments/AppStore.Authentication.Service.Contracts.1.16.1109.1.msi',
        'dfspath' => '\\\\uktee01-clusdb.dotcom.tesco.org\\IGHSBuildOutput\\AppStore.Authentication.Contracts\\AppStore.Authentication.Contracts_1.16.1109.1\\AppStore.Authentication.Service.Contracts.1.16.1109.1.msi'
},
{
  'download_path' => 'D:/Deployments/AppStore.Authentication.Service.Library.1.17.0109.1.msi',
      'dfspath' => '\\\\uktee01-clusdb.dotcom.tesco.org\\IGHSBuildOutput\\AppStore.Authentication.Library\\AppStore.Authentication.Library_1.17.0109.1\\AppStore.Authentication.Service.Library.1.17.0109.1.msi'
},
{
  'download_path' => 'D:/Deployments/AppStore.Authentication.Service.WcfHost.1.16.1226.2.msi',
      'dfspath' => '\\\\uktee01-clusdb.dotcom.tesco.org\\IGHSBuildOutput\\AppStore.Authentication.WcfHost\\AppStore.Authentication.WcfHost_1.16.1226.2\\AppStore.Authentication.Service.WcfHost.1.16.1226.2.msi'
},
{
  'download_path' => 'D:/Deployments/Tesco.Com.Core.1.16.1024.1.msi',
      'dfspath' => '\\\\uktee01-clusdb.dotcom.tesco.org\\IGHSBuildOutput\\Tesco.Com.Core\\Tesco.Com.Core_1.16.1024.1\\Tesco.Com.Core.1.16.1024.1.msi'
},
{
  'download_path' => 'D:/Deployments/AppStore.Common.Library.1.16.0307.11.msi',
      'dfspath' => '\\\\uktee01-clusdb.dotcom.tesco.org\\IGHSBuildOutput\\AppStore.Common.Library\\AppStore.Common.Library_1.16.0307.11\\AppStore.Common.Library.1.16.0307.11.msi'
}

]
