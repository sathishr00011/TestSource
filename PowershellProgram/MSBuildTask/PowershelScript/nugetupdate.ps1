param(
[string]$path="D:\Nugetpackage\NugetPackagesexisting"
)
$u = New-Object System.Collections.ArrayList
$z=Get-ChildItem -Filter *.nupkg -Path $path -Recurse | Select-Object -ExpandProperty FullName

foreach($file in $z)
{
set-location "D:\NuGet2.6"
./NuGet.exe push $file b078a0cb-e7bf-3420-a902-577c7b083653 -s https://nexus.dev.global.tesco.org/nexus/service/local/nuget/46_InternationalGrocery/

#$y=Get-Content $file;

#$file = "E:\wixfileupdateautomation\Notification\Setup\Setup.wixproj"
#$replacevalue = @'
#<Target Name="BeforeBuild" Condition="'$(BuildingInsideVisualStudio)' != 'true'">
#'@
#$readfile = gc $file
#$readfile -replace '<Target Name="BeforeBuild">',$replacevalue | Set-Content $file

}
