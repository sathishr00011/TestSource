"Microsoft (R) TfsBuild Version 10.0.0.0
for Microsoft Visual Studio v10.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Build number: 243276
    Updated Build number: Dev.Subscription_Website_Globaldev_Nexus_0.16.1125.1
Succeeded" | Out-File build.log
$file = gc build.log 
$version=$file -match "Updated*." | % {($_ -replace "^.*: ","")}
"PACKAGE_NAME=$version" | set-content build.properties