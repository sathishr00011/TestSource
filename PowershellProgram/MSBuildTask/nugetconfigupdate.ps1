param(
[string]$path="D:\InternationalSharedLibraries\Main\Tesco\Com"
)
$u = New-Object System.Collections.ArrayList
$z=Get-ChildItem -Filter NuGet.Config -Path $path -Recurse | Select-Object -ExpandProperty FullName

$u=Get-ChildItem -Filter NuGet.targets -Path $path -Recurse | Select-Object -ExpandProperty FullName

if ($z -like '*Config*' )

{


foreach($file in $z)
{

$source="D:\TestNuget\Test\.nuget\NuGet.Config"
Copy-Item $source -Destination $file -Force -Recurse


#Xcopy /I /Y $source $file


#$y=Get-Content $file;

#$file = "E:\wixfileupdateautomation\Notification\Setup\Setup.wixproj"
#$replacevalue = @'
#<Target Name="BeforeBuild" Condition="'$(BuildingInsideVisualStudio)' != 'true'">
#'@
#$readfile = gc $file
#$readfile -replace '<Target Name="BeforeBuild">',$replacevalue | Set-Content $file

}

}

if ($u -like '*targets*')

{


foreach($file in $u)
{

$source="D:\TestNuget\Test\.nuget\NuGet.targets"
Copy-Item $source -Destination $file -Force -Recurse



}



}



write-host "copied succssfully"
