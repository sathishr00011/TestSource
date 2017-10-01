#$wmidiskblock = { 
#Param($ComputerName = "LocalHost") 
#Get-WmiObject -ComputerName $ComputerName -Class win32_logicaldisk |  Where-Object {$_.drivetype -eq 3} 
#} 
 
 $wmidiskblock = {
 Param($command="HOSTNAME")
 Get-Service -ComputerName $command
 Start-Sleep -s 10
 }
 
$Computers = @("DVGEOBUILD008UK","DVGEOBUILD007UK")  
 #Start all jobs 
ForEach($Computer in $Computers) 
{ 
$Computer 
   Start-Job -scriptblock $wmidiskblock  -ArgumentList $Computer
} 
Get-Job | Wait-Job 
$out = Get-Job | Receive-Job 
$out |export-csv log.txt -Force