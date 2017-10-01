$user = "dotcom\jv37"
$password = 'Tesco$$@@!!12345678'
$Computers = @("UKDBT66BUILD05V.dotcom.tesco.org","VDI01T11HSC-580.dotcom.tesco.org") 

#create script block to run chef client
$run_chef_client = { 
Param($ComputerName, $user, $password) 
 cd ${WORKSPACE}
 knife winrm $ComputerName 'chef-client -c c:/chef/client.rb' -m -x $user -P $password 
} 
 
#Start all client excution 
ForEach($Computer in $Computers) 
{ 
  Start-Job -scriptblock $run_chef_client -ArgumentList $Computer, $user, $password
} 

#wait job until all clients execution completed
Get-Job | Wait-Job
Get-Job | Receive-Job 
Get-Job | % {if($_.state -eq 'Failed'){throw}}