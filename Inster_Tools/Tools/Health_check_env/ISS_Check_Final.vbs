Set oFSO = CreateObject("Scripting.FilesyStemObject")
outputMessage = ""
Outputfile="D:\DBT\SCRIPT\DBT_ServiceHealthCheck.html"

Set ofile = ofso.createTextFile(OutputFile, True)

outputMessage =  outputMessage &  "<table border=1 style=background-color:""#ffffcc""><tr style=background:#35586C;><th colspan=5 align=center  style=color:White>API Framework</th></tr>"
outputMessage =  outputMessage &   "<tr style=background:#35586C;><th colspan=5 align=center  style=color:White>DBT : ISS Health Check Report on " & Date() & " at " & Time() &"</th></tr>"


arraystrcomputer = array("UKDBT67APIAP02v:65","UKDBT67APIAP02v:81","UKDBT67APIAP05v:99") 

for each objcomputer in arraystrcomputer 


Set objHTTP = CreateObject("MSXML2.XMLHTTP") 
strurl = "http://" &objcomputer& "" 
objHTTP.Open "GET", strURL, FALSE 
objHTTP.Send 
  if objHTTP.statusText = "OK" then 
    outputMessage =  outputMessage &   "<tr class=status_passed> style=background:#CD5555>;<th width=150>" & objcomputer & "</th><td colspan=5 align=center>Service is UP</td><tr>"
   
   else 
   outputMessage =  outputMessage &   "<tr class=status_passed><td colspan=5 align=center>No Service is UP</td><tr>"
  end if 

  
Next

ofile.writeline outputMessage
