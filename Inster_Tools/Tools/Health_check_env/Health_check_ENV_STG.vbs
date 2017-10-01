'Create the file Objects
On Error Resume Next
Set oFSO = CreateObject("Scripting.FilesyStemObject")
Set iFSO = CreateObject("Scripting.FilesyStemObject")
InputFile=".\Servers_STG.txt"

'Declare 'outputMessage' variable to capture HTML message
outputMessage = ""
outputMessage1 = ""
Outputfile=".\STG_DriveCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set ifile = iFSO.OpenTextFile(inputfile)


Const MBCONVERSION= 1048576

' Write the HTML to design the Table
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:Blue;><th colspan=5 align=center  style=color:White>GOI</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:Blue;><th colspan=5 align=center  style=color:White>STG|Grocery Online International|  LOW DISK SPACE REPORTED On " & Date() & " at " & Time() &"</th></tr><tr class=status_passed><th colspan=5 align=right>Disk Space Alerts &lt; 10% (Marked as Red)</th></tr><tr><th width=150>Server</th><th width=150>Drive</th><th width=150>Disk Size (GB)</th><th width=150>FreeSpace (GB)</th><th width=150>%</th></tr>"


alert = 0
Do until ifile.AtEndOfLine
    strComputer = ifile.ReadLine
     GetDiskspace(strComputer)
   
Loop 


outputMessage =  outputMessage &   "</table> <br/><br/>" 

Set ifile = iFSO.OpenTextFile(inputfile)
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>Grocery Online International</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>STG :CPU useage & Memory Useage " & Date() & " at " & Time() &"</th></tr>"

outputMessage =  outputMessage &   "<tr><th width=150 >Server Name</th><th width=150>CPU USAGE</th><th width=150>MEMMORY USAGE</th></tr>"
Do until ifile.AtEndOfLine
    StrComputer = ifile.ReadLine
    iprocessor= GetProcessorUsage(strComputer)
    iMemUsage = GetMemUsage(strComputer) 


outputMessage =  outputMessage &   "<tr> <td width=150>" & strComputer & "</td><td  align=center>" & iprocessor & "%</td><td  align=center>" & iMemUsage & "%</td></tr>"

Loop

ofile.writeline outputMessage

Function GetDiskspace(Computer)

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
            
        else
outputMessage =  outputMessage &   "<tr style=background:#D8F781;><td>" & Computer & "</td><td align=center>" & objLogicalDisk.DeviceID &_
                                "</td><td align=center>" & round(objLogicalDisk.size/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                round(objLogicalDisk.freespace/MBCONVERSION/1024,2) & "</td><td align=center>" &_
                                 round(((objLogicalDisk.freespace/MBCONVERSION/1024)/(objLogicalDisk.size/MBCONVERSION/1024))*100,2) & "</td></tr>"
                                 
                                end  if
                                 
         
        end if

    Next

End Function


Function GetProcessorUsage(Computer)


Set objWMIService = GetObject("winmgmts:\\" & Computer& "\root\CIMV2") 



Set colItems1 = GetObject("WinMgmts:\\" & Computer& "\root\CIMV2").ExecQuery("Select * FROM Win32_OperatingSystem ") 
 
Set colItems = objWMIService.ExecQuery("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name = '_Total'")

For Each objItem In colItems 


strLineProcessorTime =  objItem.PercentProcessorTime  
 
NEXT

GetProcessorUsage=strLineProcessorTime 

END Function 



Function GetMemUsage(Computer)

  Const FLAGS = 48 ' wbemFlagForwardOnly + wbemFlagReturnImmediately

  Dim oLocator, oWMI, colItems, oItem, nTotalMem, nFreeMem

 
 
  Set oLocator = CreateObject("WbemScripting.SWbemLocator")

  Set oWMI = oLocator.ConnectServer(Computer, "root\cimv2", Login, Password)

  oWMI.Security_.ImpersonationLevel = 3 ' wbemImpersonate

 
 
  ' Get total physical memory, in kbytes

  Set colItems = oWMI.ExecQuery("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem", , FLAGS)

  For Each oItem in colItems 
 
    nTotalMem = CDbl(oItem.TotalPhysicalMemory) / 1024

  Next

 
 
  ' Get free physical memory, in kbytes

  Set colItems = oWMI.ExecQuery("SELECT FreePhysicalMemory FROM Win32_OperatingSystem", , FLAGS) 
 
  For Each oItem in colItems 
 
    nFreeMem = CDbl(oItem.FreePhysicalMemory)

  Next

 
 
  ' Memory Usage = (Total - Free) / Total

   GetMemUsage = Round((nTotalMem - nFreeMem) / nTotalMem * 100)

End Function

Set objMessage = CreateObject("CDO.Message") 
objMessage.Subject = "STG Disk Space and CPU Utilization" 
objMessage.From = "rino.raju@in.tesco.com" 
objMessage.To = "rino.raju@in.tesco.com" 
objMessage.Cc="rino.raju@in.tesco.com"
objMessage.HTMLBody = outputMessage 

'==This section provides the configuration information for the remote SMTP server.
'==Normally you will only change the server name or IP.
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 

'Name or IP of Remote SMTP Server
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "DBT80MAIL01"

objMessage.Configuration.Fields.Update

objMessage.Send


