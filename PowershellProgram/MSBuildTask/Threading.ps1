$wmidiskblock = { 
Param($ComputerName = "LocalHost") 
Get-Service -ComputerName $ComputerName 
} 
 
 
$Computers = @("DVGEOBUILD008UK","DVGEOBUILD007UK", "test")  
 #Start all jobs 
ForEach($Computer in $Computers) 
{ 
$Computer 
   Start-Job -scriptblock $wmidiskblock  -ArgumentList $Computer 
} 
Get-Job | Wait-Job
Get-Job | % {if($_.state -eq 'Failed'){throw}}
$out = Get-Job | Receive-Job 
$out |export-csv log.txt