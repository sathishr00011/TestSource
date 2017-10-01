Set objMessage = CreateObject("CDO.Message") 
objMessage.Subject = "Example CDO Message" 
objMessage.From = "sandhya.sivaramakrishnan@uk.tesco.com" 
objMessage.To = "sandhya.sivaramakrishnan@uk.tesco.com" 
objMessage.Cc="Abishek.Kakol@in.tesco.com"
objMessage.TextBody = "This is some sample message text."

'==This section provides the configuration information for the remote SMTP server.
'==Normally you will only change the server name or IP.
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 

'Name or IP of Remote SMTP Server
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "172.25.58.118"

objMessage.Configuration.Fields.Update

objMessage.Send


