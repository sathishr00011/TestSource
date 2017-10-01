$lines = Get-Content D:\boost\master.txt
$style="<TABLE  align=center style=WIDTH:75%;><TR><TD style=TEXT-ALIGN:center;BACKGROUND-COLOR:#2A19E8;VERTICAL-ALIGN:middle colspan=5><H2 style=COLOR:white><STRONG><br>Code Coverage Report</STRONG></H2></TD></TR><TR height=30><TD bgColor=#2A19E8 style=COLOR:white><STRONG><center>Component NAME</center></STRONG></TD><TD bgColor=#2A19E8 style=COLOR:white><STRONG><center>DLL NAME</center></STRONG></TD><TD bgColor=#2A19E8 style=COLOR:white><STRONG><center>Blocks Covered</center></STRONG></TD><TD bgColor=#2A19E8 style=COLOR:white><STRONG><center>Blocks Not Covered</center></STRONG></TD><TD bgColor=#2A19E8 style=COLOR:white><STRONG><center>Coverage</center></STRONG></TD></TR>"
for($i=0;$i -le $lines.length-1;$i++)
{
$html=get-content $lines[$i]
$count = (Get-Content $lines[$i] | select-string -pattern "Tesco.*.dll").length
$dllname=get-content $lines[$i] | where { $_ -match "Tesco.*.dll"} | foreach {$matches[0]}
$blocks_covered=get-content $lines[$i] | where { $_ -match ".dll<\/td><td.*?>(.+?)<\/td><td.*?>(.+?)<\/td>"} | foreach {$matches[1]}
$blocks_not_covered=get-content $lines[$i] | where { $_ -match ".dll<\/td><td.*?>(.+?)<\/td><td.*?>(.+?)<\/td>"} | foreach {$matches[2]}
$component_name=get-content $lines[$i] | where { $_ -match "Tesco.Com.([A-Za-z]+).([A-Za-z]+.[A-Za-z]+).*.dll"} | foreach {$matches[2]}
$Coverage=$html -match ".dll" | ForEach-Object { $_ -replace "^.*>"}| % {$_ -replace "%"}
if($count -eq 0 -or $count -eq $null)
{
$count=1
if($([int]$Coverage) -lt 80)
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#B9DDF5;><font face=verdana size=2><strong>$($component_name)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered)</strong></font></td><TD style=BACKGROUND-COLOR:#FF0000;><font face=verdana size=2><strong>$($Coverage)%</strong></font></td>"
}
else
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#B9DDF5;><font face=verdana size=2><strong>$($component_name)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered)</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered)</strong></font></td><TD style=BACKGROUND-COLOR:#00FF78;><font face=verdana size=2><strong>$($Coverage)%</strong></font></td>"
}
}
else
{
for($j=0;$j -le $count-1;$j++)
{
if($([int]$Coverage[$j]) -lt 80)
{
if($j -eq 0)
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#B9DDF5; rowspan=$($count)><font face=verdana size=2><strong>$($component_name[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#FF0000;><font face=verdana size=2><strong>$($Coverage[$j])%</strong></font></td>"
}
else
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#FF0000;><font face=verdana size=2><strong>$($Coverage[$j])%</strong></font></td>"
}
}
else
{
if($j -eq 0)
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#B9DDF5; rowspan=$($count)><font face=verdana size=2><strong>$($component_name[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#00FF78;><font face=verdana size=2><strong>$($Coverage[$j])%</strong></font></td>"
}
else
{
$style=$style+"<TR height=40><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($dllname[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#E6E6E6;><font face=verdana size=2><strong>$($blocks_not_covered[$j])</strong></font></td><TD style=BACKGROUND-COLOR:#00FF78;><font face=verdana size=2><strong>$($Coverage[$j])%</strong></font></td>"
}
}
}
}
}
$style=$style+"</tr></table></body></html>" | out-file D:\boost\report.html
Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted -Force

$web = New-Object System.Net.WebClient

#$ToAddress = "sathishkumar.x.ramaraj@in.tesco.com"
#$ToAddress = "kannaiah.dasari@in.tesco.com"
$ToAddress = "HSC_Boost_Engineering@in.tesco.com,HSC_DEVOps_Rewards@in.tesco.com"
$Subject = "Rewards Unit Test Code Coverage Report"
$Body = "PF the Code Coverage Report attached."
$emailattachment = "D:\boost\report.html"


Write-Host "Start : Sending Email" (Get-Date).ToString()

     #SMTP server name
     $smtpServer = "172.25.58.118"

     #Creating a Mail object
    $msg = new-object Net.Mail.MailMessage
   $msg.IsBodyHtml = 1 
     
     #Creating SMTP server object
     $smtp = new-object Net.Mail.SmtpClient($smtpServer)

     #Email structure 
    $msg.From = "HSC_DEVOps_Rewards@in.tesco.com"
    $msg.To.Add($ToAddress)
    $msg.subject = $Subject
    $msg.body = get-content $emailattachment
    $smtp.Send($msg)
  
Write-Host "`nDONE  : Sending Email" (Get-Date).ToString()