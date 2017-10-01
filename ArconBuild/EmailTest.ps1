Param($status, $component, $version)

Set-ExecutionPolicy RemoteSigned

function sendMail{

     Write-Host "Sending Email"

     #SMTP server name
     $smtpServer = "172.25.58.118"

     #Creating a Mail object
     $msg = new-object Net.Mail.MailMessage

     #Creating SMTP server object
     $smtp = new-object Net.Mail.SmtpClient($smtpServer)

     #Email structure 
     $msg.From = "HSC_DEVOps_Rewards_CD@in.tesco.com"
     $msg.To.Add("HSC_DEVOps_Rewards@in.tesco.com")
     $msg.subject = "Nolio Deployment Status"
     $msg.body = $component + " - " + $version +  " - Deployment Status : " + $status

     #Sending email 
     $smtp.Send($msg)
  
}

#Calling function
sendMail