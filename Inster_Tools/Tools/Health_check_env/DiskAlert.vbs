



'Create the file Objects
Set oFSO = CreateObject("Scripting.FilesyStemObject")
Set iFSO = CreateObject("Scripting.FilesyStemObject")
InputFile="D:\DBT\SCRIPT\Servers.txt"

'Declare 'outputMessage' variable to capture HTML message
outputMessage = ""
Outputfile="D:\DBT\SCRIPT\DBT_DriveCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set ifile = iFSO.OpenTextFile(inputfile)


Const MBCONVERSION= 1048576

' Write the HTML to design the Table
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:Blue;><th colspan=5 align=center  style=color:White>API Framework</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:Blue;><th colspan=5 align=center  style=color:White>DBT | API FRAMEWORK|  LOW DISK SPACE REPORTED On " & Date() & " at " & Time() &"</th></tr><tr class=status_passed><th colspan=5 align=right>Disk Space Alerts &lt; 10% (Marked as Red)</th></tr><tr><th width=150>Server</th><th width=150>Drive</th><th width=150>Disk Size (GB)</th><th width=150>FreeSpace (GB)</th><th width=150>%</th></tr>"


alert = 0
Do until ifile.AtEndOfLine
    Computer = ifile.ReadLine
    Set objWMIService = GetObject("winmgmts://" & Computer)
    Set colLogicalDisk = objWMIService.ExecQuery("Select * from Win32_LogicalDisk WHERE DriveType=3")
   outputMessage =  outputMessage &   "<tr style=background:Blue;><th colspan=5 align=center  style=color:White>Disk Space Status on "& Computer &"</th></tr>"
    For Each objLogicalDisk In colLogicalDisk
        if objLogicalDisk.drivetype=3 then   
             if ((objLogicalDisk.freespace/MBCONVERSION)/(objLogicalDisk.size/MBCONVERSION))*100 < 10 then
             
	                outputMessage =  outputMessage &   "<tr  style=background:Red;><td>" & Computer & "</td><td align=center>" & objLogicalDisk.DeviceID &_
                                "</td><td align=center>" & round(objLogicalDisk.size/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                round(objLogicalDisk.freespace/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                 round(((objLogicalDisk.freespace/MBCONVERSION/1024)/(objLogicalDisk.size/MBCONVERSION/1024))*100,2) & "</td></tr>"
             'alert=1
	     'end if
        else
 outputMessage =  outputMessage &   "<tr><td>" & Computer & "</td><td align=center>" & objLogicalDisk.DeviceID &_
                                "</td><td align=center>" & round(objLogicalDisk.size/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                round(objLogicalDisk.freespace/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                 round(((objLogicalDisk.freespace/MBCONVERSION/1024)/(objLogicalDisk.size/MBCONVERSION/1024))*100,2) & "</td></tr>"
                                 
                                end  if
                                 
         
        end if
    Next
Loop 

' There will no Disk Alert if Alert is 0
'if alert=0 then 
'outputMessage =  outputMessage &   "<tr><td colspan=5 align=center>No Disk Alerts</td><tr>"
'end if

outputMessage =  outputMessage &   "</table>"

ofile.writeline outputMessage
' Send the mail If Services are down

If  Servicealert = 1 or  Servicealert = 2 then
Set objEmail = CreateObject("CDO.Message")

objEmail.From ="rino.raju@in.tesco.com"
objEmail.To ="samrudhi.sheth@in.tesco.com"
objEmail.Subject = "*** PPE | API FRAMEWORK | Service HealthChek Reported On "  & Date() & " at " & Time() & " ***"
objEmail.HTMLbody = outputMessage

objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/sendusing")=2
'Name or IP of remote SMTP server
objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserver")="UKDBT66DEMOS01V"
'Server port
objEmail.Configuration.Fields.Item("http://schemas.microsoft.com/cdo/configuration/smtpserverport")=25
objEmail.Configuration.Fields.Update
'objEmail.Send
'WScript.Echo 'Mail Send'
end if