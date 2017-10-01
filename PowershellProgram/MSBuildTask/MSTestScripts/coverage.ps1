#$path=$env:ReportPath
#$html=Get-Content $path
$html=Get-Content "D:\MSBuildTask\CodeCoverageReport\CoverageReportDifferent\CoverageReportxml\bin\Debug\Report\coverage.html"
$result=$html -match ".dll" | ForEach-Object { $_ -replace "^.*>" } | % {$_ -replace "%"} | % { if($_ -gt 20){"pass"} else {"fail"}}
if($result -eq "fail")
{
write-host "fail"
#Email Sending

Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted -Force

$web = New-Object System.Net.WebClient

$ToAddress = "sathishkumar.x.ramaraj@in.tesco.com"
#$ToAddress = "HSC_DEVOps_Rewards@in.tesco.com"
#$ToAddress = "HSC_Boost_Engineering@in.tesco.com,HSC_DEVOps_Rewards@in.tesco.com"
$Subject = "Code Coverage Report"
$Body = "PF the Sonar Report attached."
$emailattachment = "D:\MSBuildTask\CodeCoverageReport\CoverageReportDifferent\CoverageReportxml\bin\Debug\Report\coverage.html"


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
     
     #$attachment = New-Object System.Net.Mail.Attachment($emailattachment, 'text/plain')
     #$attachment = New-Object System.Net.Mail.Attachment($emailattachment)
     #$msg.Attachments.Add($attachment)
    
     #Sending email 
     $smtp.Send($msg)
  
Write-Host "`nDONE  : Sending Email" (Get-Date).ToString()
throw
}
else 
{
write-host "pass"

}
