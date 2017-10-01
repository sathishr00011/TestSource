param(
[string]$path="D:\WixChange"
)
$u = New-Object System.Collections.ArrayList
$z=Get-ChildItem -Filter *.wixproj -Path $path -Recurse | Select-Object -ExpandProperty FullName

foreach($file in $z)
{
[xml]$y=Get-Content $file;
#$file = "E:\wixfileupdateautomation\Notification\Setup\Setup.wixproj"
$replacevalue = @'
<Target Name="BeforeBuild" Condition="'$(BuildingInsideVisualStudio)' != 'true'">
'@
$readfile = gc $file
$readfile -replace '<Target Name="BeforeBuild">',$replacevalue | Set-Content $file

}