Param($ToAddress,$Subject, $Body )

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
     $msg.To.Add($ToAddress)
     $msg.subject = $Subject
     $msg.body = $Body
     #Sending email 
     $smtp.Send($msg)
  
}

#Calling function
sendMail