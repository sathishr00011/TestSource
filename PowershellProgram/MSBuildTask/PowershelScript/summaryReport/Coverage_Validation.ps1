$html=Get-Content "D:\Buildoutput\Online_UI\Report\coverage.htm"
$result=$html -match ".dll" | ForEach-Object { $_ -replace "^.*>" } | % {$_ -replace "%"} | % { if($_ -gt 80){"failed"}}
if($result -ne $null)
{
write-host "pass"
}
else 
{
write-host "fail"
}
