$RMpath=$env:RMFile
$ComponentName=$env:Component
$xml = [xml](Get-Content $RMpath)
$nodes = $xml.SelectNodes("//Component[@ComponentName=""$ComponentName"" and @State=""ToBeInstalled""]")
$NolioParameter =$Null
foreach ($node in $nodes )
 { 
$NolioParameter = $node.ComponentManifest.Substring($node.ComponentManifest.LastIndexOf("_")+1)
$NolioParameter = $NolioParameter.Substring(0,$NolioParameter.LastIndexOf("."))
$NolioParameter = $node.NolioParameterPrefix + $NolioParameter 
#$node.State="Installed"
$node.Deployed=$NolioParameter
$node.DeploymentStatus="Progress"
}
if ($NolioParameter) {write-host $NolioParameter} else {$NolioParameter="Nothing"}
"NOLIOARGUMENT=$NolioParameter" | Out-File env.properties -Encoding ASCII
$xml.Save($RMpath)