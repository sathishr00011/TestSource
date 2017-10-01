try 
{
# Run chef client

#knife winrm 'UKDBT66BUILD05V.dotcom.tesco.org' 'chef-client -c c:/chef/client.rb' -m -x dotcom\jv37 -P 'Tesco$$@@!!12345678'
#knife winrm 'DVMARDGWEB001UK.dev.global.tesco.org' 'chef-client -c c:/chef/client.rb' -m -x globaldev\a-wy42 -P globaldev_password%

$user = "globaldev\a-wy42"
$password = $env:globaldev_password
$Computers = @("DVMARDGWEB001UK.dev.global.tesco.org") 

#create script block to run chef client
$run_chef_client = { 
Param($ComputerName, $user, $password) 
try
{
cd $env:WORKSPACE
knife winrm $ComputerName 'chef-client -c c:/chef/client.rb' -m -x $user -P $password 
if($lastexitcode -ne 0) { throw }
}
catch
{
$Errormessage=$_.Exception.Message
throw $Errormessage 
}
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
}
catch
{
$Errormessage=$_.Exception.Message
throw $Errormessage 
}
