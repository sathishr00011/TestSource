$path="D:\MSBuildTask\Nugetfiles"
$source="D:\MSBuildTask\Nugetupgrade"

$Temp=$path + "\.nuget\NuGet.Config"
$Tempdist=$source + "\dist"

#Copy-Item -Path $path + "\.nuget\NuGet.Config" -Destination $source + "\.nuget\NuGet.Config" -Force -Recurse

Xcopy /I $Temp $Tempdist