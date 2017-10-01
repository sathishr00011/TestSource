
On Error Resume Next
'Create the file Objects
Set oFSO = CreateObject("Scripting.FilesyStemObject")
Set iFSO = CreateObject("Scripting.FilesyStemObject")
InputFile=".\Servers_dev.txt"

'Declare 'outputMessage' variable to capture HTML message
outputMessage = ""
Outputfile=".\WindowsServiceHealthCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set ifile = iFSO.OpenTextFile(inputfile)

'Declare mail variable


'Declare the Service Name
strSVCName = "world wide web publishing service"
strETWSVCName = "Net.msmq listener adapter"
'strSISVCName =  "WMI Performance Adapter"
strTescoVCName =  "application management"


set colServices = Nothing
set colAppFabricServices = Nothing

' Write the HTML to design the Table
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>Grocery Online International</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>DEV/DBT : Windows Services Health Check Report " & Date() & " at " & Time() &"</th></tr>"


Servicealert = 0
Do until ifile.AtEndOfLine
    Computer = ifile.ReadLine
    Set objWMIService = GetObject("winmgmts://" & Computer)
   
    'Get Windows Service Details
        Set colServices = objWMIService.ExecQuery("Select * from Win32_Service WHERE DisplayName = '" & strSVCName  & "' or DisplayName = '" & strETWSVCName  & "' or DisplayName ='" & strTescoVCName & "'")
        strServiceServer = Computer
   
    ' Windows Service Check Starts here
        If Not colServices Is Nothing then 
        'WScript.Echo Computer
        checkServiceCounter = 0
	        ' Write the HTML for capture the Service Details
	        outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>Windows Service Status on "& strServiceServer &"</th></tr>"
	        outputMessage =  outputMessage &   "<tr><th width=150>Service Name</th><th width=150>Status</th><th width=150>Start Mode</th></tr>" 

        'Get the required Services Details
        For Each objService in colServices
              if objService.State = "Running" then   
                outputMessage =  outputMessage &   "<tr style=background:#D8F781;><td align=center>" & objService.displayName &_
                             "</td><td align=center>" &_
                             objService.State & "</td><td align=center>" & objService.StartMode & "</td></tr>"
             else
              	 outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & objService.displayName &_
                             "</td><td align=center>" &_
                             objService.State & "</td><td align=center>" & objService.StartMode & "</td></tr>"
            end if
         checkServiceCounter = checkServiceCounter + 1
        Next

        if checkServiceCounter = 0 then 
        outputMessage =  outputMessage &   "<tr class=status_passed><td colspan=5 align=center>No Service Available</td><tr>"
        end if

        end if
        ' Windows Service Check Ends here

Loop 


outputMessage =  outputMessage &   "</table>"

ofile.writeline outputMessage
' Send the mail If Services are down



Set objMessage = CreateObject("CDO.Message") 
objMessage.Subject = "IGHS|DEV/DBT Service HealthChek Report"
objMessage.From = "HSC_Devops_IGHS@in.tesco.com" 
objMessage.To = "Revanasiddappa.anabi@in.tesco.com;shibabrata.bhattacharjee@in.tesco.com;chethan.s.kumar@in.tesco.com" 
objMessage.Cc="HSC_Devops_IGHS@in.tesco.com"
objMessage.HTMLBody = outputMessage 

 'IGHS_team_jaguars@in.tesco.com 

'==This section provides the configuration information for the remote SMTP server.
'==Normally you will only change the server name or IP.
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 

'Name or IP of Remote SMTP Server
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "172.25.58.118"

objMessage.Configuration.Fields.Update

objMessage.Send














'If  Servicealert = 1 or  Servicealert = 2 then
'Set objEmail = CreateObject("CDO.Message")

'objEmail.From = "IGHS_Team_Jaguars@in.tesco.com"
'objEmail.To = "IGHS_SCRUM_MASTERS@uk.tesco.com;Karpagavalli.Jaykumar@in.tesco.com"
'objEmail.Subject = "IGHS|DEV/DBT Service HealthChek Reported On "  & Date() & " at " & Time() & " ***"
'objEmail.HTMLbody = outputMessage

'objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusing")=2
'Name or IP of remote SMTP server
'objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver")="dbt97mail01v.dotcom.tesco.org"
'Server port
'objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport")=25
'objEmail.Configuration.Fields.Update
'objEmail.Send
'WScript.Echo 'Mail Send'
'end if