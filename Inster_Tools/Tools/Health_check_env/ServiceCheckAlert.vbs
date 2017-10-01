



'Create the file Objects
Set oFSO = CreateObject("Scripting.FilesyStemObject")
Set iFSO = CreateObject("Scripting.FilesyStemObject")
InputFile="D:\Tools\Health_check_env\Servers.txt"

'Declare 'outputMessage' variable to capture HTML message
outputMessage = ""
Outputfile="D:\Tools\Health_check_env\DBT_ServiceHealthCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set ifile = iFSO.OpenTextFile(inputfile)

'Declare mail variable

sendMailFrom = "  "
sendMailTo = " "

'Declare the Service Name
strSVCName = "DotnetProcessLayerService"
strETWSVCName = "ETWConsumer"
strSISVCName =  "StreamInsight"
strTescoVCName =  "TescoAPIAdminSynchronizer"


set colServices = Nothing
set colAppFabricServices = Nothing

' Write the HTML to design the Table
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>API Framework</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>DBT : Service Health Check Report on " & Date() & " at " & Time() &"</th></tr>"


Servicealert = 0
Do until ifile.AtEndOfLine
    Computer = ifile.ReadLine
    Set objWMIService = GetObject("winmgmts://" & Computer)
   
    'Get Windows Service Details
        Set colServices = objWMIService.ExecQuery("Select * from Win32_Service WHERE Name = '" & strSVCName  & "' or Name = '" & strETWSVCName  & "'  or Name = '" & strSISVCName & "' or Name ='" & strTescoVCName & "' or Name = 'AppFabricCachingService'")
        strServiceServer = Computer
   
    ' Windows Service Check Starts here
        If Not colServices Is Nothing then 
        'WScript.Echo Computer
        checkServiceCounter = 0
	        ' Write the HTML for capture the Service Details
	        outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>Windows Service Status on "& strServiceServer &"</th></tr>"
	        outputMessage =  outputMessage &   "<tr><th width=150 >Server Name</th><th width=150>Display Name</th><th width=150>Service Name</th><th width=150>Status</th><th width=150>Start Mode</th></tr>" 

        'Get the required Services Details
        For Each objService in colServices
              if objService.State = "Running" then   
                outputMessage =  outputMessage &   "<tr><td>" & strServiceServer & "</td><td align=center>" & objService.DisplayName &_
                             "</td><td align=center>" & objService.Name & "</td><td align=center>" &_
                             objService.State & "</td><td align=center>" & objService.StartMode & "</td></tr>"
             else
                outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td>" & strServiceServer & "</td><td align=center>" & objService.DisplayName &_
                             "</td><td align=center>" & objService.Name & "</td><td align=center>" &_
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

If  Servicealert = 1 or  Servicealert = 2 then
Set objEmail = CreateObject("CDO.Message")

objEmail.From = "rino.raju@in.tesco.com"
objEmail.To = "samrudhi.sheth@in.tesco.com"
objEmail.Subject = "*** PPE | API FRAMEWORK | Service HealthChek Reported On "  & Date() & " at " & Time() & " ***"
objEmail.HTMLbody = outputMessage

objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusing")=2
'Name or IP of remote SMTP server
objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver")="dbt97mail01v.dotcom.tesco.org"
'Server port
objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport")=25
objEmail.Configuration.Fields.Update
'objEmail.Send
'WScript.Echo 'Mail Send'
end if