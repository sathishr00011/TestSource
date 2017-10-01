
On Error Resume Next
Set oFSO = CreateObject("Scripting.FilesyStemObject")
Set iFSO = CreateObject("Scripting.FilesyStemObject")
InputFile=".\iis_server_list.txt"

'Declare 'outputMessage' variable to capture HTML message
outputMessage = ""
outputMessage1 = ""
Outputfile=".\IIS_AppPool_Status.html"

Set ofile = ofso.createTextFile(OutputFile, True)
Set ifile = iFSO.OpenTextFile(inputfile)




' Write the HTML to design the Table
outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>Grocery Online International</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>DBT/DEV : IIS AppPool Report on " & Date() & " at " & Time() &"</th></tr>"


Do until ifile.AtEndOfLine
    
    strComputer = ifile.ReadLine


'option explicit

'dim strcomputer, objWMIservice, colitems, objitem,locatorObj,ProviderObj,strQueryAppPools'




 ' strComputer = "UKDEV66MIGAP01V"

		Const WbemAuthenticationLevelPktPrivacy = 6 

		Set locatorObj = CreateObject("WbemScripting.SWbemLocator") 
		locatorObj.Security_.AuthenticationLevel = WbemAuthenticationLevelPktPrivacy 
  
		Set ProviderObj = locatorObj.ConnectServer(strComputer, "root\webadministration") 

Set strQueryAppPools = providerobj.ExecQuery("select * from applicationpool")



  outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>AppPool Status on "& strComputer &"</th></tr>"
	        outputMessage =  outputMessage &   "<tr><th width=200>AppPool Name</th><th width=150>Status</th></tr>" 



For Each oAppPool in strQueryAppPools

Select Case oAppPool.AppPoolState
				  Case 1
				   poolState = "Starting"
				   outputStatus = "WARNING: "
				   outputCode = 50
				  Case 2
			   poolState = "Running"
			   outputStatus = "OK: "
			   outputCode = 100
			  Case 3		
			   poolState = "Stopping"
			   outputStatus = "WARNING: "
			   outputCode = 50
				  Case 4
			   poolState = "Stopped"
			   outputStatus = "CRITICAL: "
			   outputCode = 0
			 Case else
			   poolState = "unknown"
			   outputStatus = "UNKNOWN: "
			   outputCode = 0
			End Select

     'wscript.echo "name: " & oAppPool.name
'wscript.echo "state: " & oAppPool.GetState
if oAppPool.GetState = 1 then   

poolname1=right(oapppool.Name,22)
		 poolname2=mid(poolname1,instr(poolname1,"/")+1,18)
  
		  outputMessage =  outputMessage &   "<tr style=background:#66CC33;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>Running</td></tr>"
		 
		             else if oAppPool.GetState = 0 then
              		             outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>Starting</td></tr>"

			else if  oAppPool.GetState = 3 then
              		             outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>Stopped</td></tr>"
'wscript.echo "3 i am here"
			else if oAppPool.GetState = 4 then
              		             outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>unknown</td></tr>"


			else if oAppPool.GetState = 2 then
              		             outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>Stopping</td></tr>"
			else 
              		             outputMessage =  outputMessage &   "<tr  style=background:#CD5555;><td align=center>" & oAppPool.name &_
                             "</td><td align=center>Service not found</td></tr>"



           end if
		   end if
		   end if
		   end if
		   end if


next

'strQueryAppPools=""

Loop


outputMessage =  outputMessage &   "</table>"

ofile.writeline outputMessage
' Send the mail If Services are down

'If  Servicealert = 1 or  Servicealert = 2 then
Set objMessage = CreateObject("CDO.Message") 
objMessage.Subject = "DEV/DBT IIS AppPool Status " 
objMessage.From = "HSC_Devops_IGHS@in.tesco.com" 
objMessage.To = "Revanasiddappa.anabi@in.tesco.com;shibabrata.bhattacharjee@in.tesco.com;chethan.s.kumar@in.tesco.com" 
objMessage.Cc="HSC_Devops_IGHS@in.tesco.com"
objMessage.HTMLBody = outputMessage 

'==This section provides the configuration information for the remote SMTP server.
'==Normally you will only change the server name or IP.
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 

'Name or IP of Remote SMTP Server
objMessage.Configuration.Fields.Item _
("http://schemas.microsoft.com/cdo/configuration/smtpserver") = "172.25.58.118"

objMessage.Configuration.Fields.Update

objMessage.Send


