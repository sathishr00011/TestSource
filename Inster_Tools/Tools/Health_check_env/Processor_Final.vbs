Set oFSO = CreateObject("Scripting.FilesyStemObject")
outputMessage = ""
Outputfile="D:\DBT\SCRIPT\DBT_ServiceHealthCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set iFSO = CreateObject("Scripting.FilesyStemObject")
strComputer="D:\DBT\SCRIPT\Servers.txt"

Set ifile = iFSO.OpenTextFile(strComputer)
	outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>API Framework</th></tr>"
	outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>DBT :CPU useage & Memory Useage " & Date() & " at " & Time() &"</th></tr>"

	outputMessage =  outputMessage &   "<tr><th width=150 >Server Name</th><th width=150>CPU USAGE</th><th width=150>MEMMORY USAGE</th></tr>"
Do until ifile.AtEndOfLine
    		StrComputer = ifile.ReadLine
		Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\CIMV2") 



		Set colItems1 = GetObject("WinMgmts:\\" & strComputer & "\root\CIMV2").ExecQuery("Select * FROM Win32_OperatingSystem ") 
 		Set colItems = objWMIService.ExecQuery("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name = '_Total'")
			For Each objItem In colItems 


				strLineProcessorTime =  objItem.PercentProcessorTime  
 


			NEXT

			iMemUsage = GetMemUsage(strComputer) 


                


			outputMessage =  outputMessage &   "<tr> <td width=150>" & strComputer & "</td><td  align=center>" & strLineProcessorTime & "</td><td  align=center>" & iMemUsage & "</td</tr>"



   


Loop


ofile.writeline outputMessage
	


Function GetMemUsage(Computer)

  	Const FLAGS = 48 ' wbemFlagForwardOnly + wbemFlagReturnImmediately
	Dim oLocator, oWMI, colItems, oItem, nTotalMem, nFreeMem
	

	Set oLocator = CreateObject("WbemScripting.SWbemLocator")
	Set oWMI = oLocator.ConnectServer(Computer, "root\cimv2", Login, Password)

  	oWMI.Security_.ImpersonationLevel = 3 ' wbemImpersonate

 
 
  ' ============Get total physical memory, in kbytes================================================

  	Set colItems = oWMI.ExecQuery("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem", , FLAGS)

  			For Each oItem in colItems 
 
    				nTotalMem = CDbl(oItem.TotalPhysicalMemory) / 1024

  			Next

 
 
  ' ============Get free physical memory, in kbytes==================================================

  	Set colItems = oWMI.ExecQuery("SELECT FreePhysicalMemory FROM Win32_OperatingSystem", , FLAGS) 
 
  			For Each oItem in colItems 
 
    				nFreeMem = CDbl(oItem.FreePhysicalMemory)

  			Next

 
 
  ' ============Memory Usage = (Total - Free) / Total=================================================

   	

	GetMemUsage = Round((nTotalMem - nFreeMem) / nTotalMem * 100)

End Function

