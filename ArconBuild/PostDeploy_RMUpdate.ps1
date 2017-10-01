$path =$env:RMPARENTPATH
#$path +="ReleaseManifest_Scheduled.xml"
$Deployed=$env:NOLIOARGUMENT 
$ComponentName=$env:COMPONENT
$DeploymentStatus=$env:DeploymentStatus
Write-Host $path
Write-Host $Deployed
Write-Host $ComponentName
$xml = [xml](Get-Content $path)
$nodes = $xml.SelectNodes("//Component[@ComponentName=""$ComponentName""]")
foreach ($node in $nodes )
{
  echo $node.ComponentManifest
  #$node.Rollback = $node.Deployed
  $node.Deployed = $Deployed
  $node.DeploymentStatus=$env:DeploymentStatus

  if($DeploymentStatus -eq "Success")
  {
	 $node.State="Installed"
  }

}
$xml.Save($path)