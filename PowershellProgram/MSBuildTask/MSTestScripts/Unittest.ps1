param ( [String]$path="Not Specified")

Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted -Force

#$path="D:\JenkinsSlave\workspace\Rewards\Search"
#$pathtestresult="D:\Buildoutput\Search"

if ($path -eq "Not Specified")
{
    write-host "Input path not specified" -ForegroundColor Red 
    exit 1
}

if (Test-Path $path)
{ }
else
{
    write-host "'$path' Incorrect Path ... Please provide valid input !!" -ForegroundColor Red 
    exit 1
}

#$filter="*Test*.dll"
$filter="EndecaImpl.Tests.dll"

$exclude="*\obj\*"
$target = "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\MSTest.exe"
#$filterarg= "+[*test*]*.dll "
$filterarg= " -filter:+[*]* "
$output = ".\TestResults\coverage-report.xml"
$targetArgs = "/testsettings:$path\local.testsettings /resultsfile:.\TestResults\Testreport.trx "

$OCargs =""
$files = get-childitem -path $path -filter $filter -recurse | Where {$_.FullName -notlike $exclude} 
foreach ($file in $files)
{ 
    $OCargs = $OCargs + '/testcontainer:' + $file.FullName + " "
}
$targetArgs=$targetArgs + $OCargs
#$strOpenCover = "`' $strOpenCover '`""
#$strOpenCover = $strOpenCover + '"'

#$cmd="D:\OpenCover.4.0.1128\OpenCover.Console.exe"
#$fullOCPath=$userprofile.Value + "\AppData\Local\Apps\OpenCover\OpenCover.Console.exe -register:user `"-target:$target`" `"-targetargs:$targetArgs`" `"-filter:$filters`" -output:$outputFileName"   
Write-Host $filter
write-host $fullOCPath
write-host $filterarg

$fullOCPath="D:\OpenCover\OpenCover.Console.exe -register:user -mergebyhash `"-target:$target`" `"-targetargs:$targetargs`" `"-filter:$filterarg`" -output:$output"

Invoke-Expression $fullOCPath  
#iex "& `"$cmd $strOpenCover`""
write-host "Open Cover executed..."
#end
exit 0