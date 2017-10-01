# This powershell script is invoking chef-client in parallel on multiple nodes.
# Values of $ENV:Chef_Environment, $ENV:Platform, $ENV:Tag, $ENV:WORKSPACE $ENV:username_winrm and $ENV:password_winrm will be fed by Jenkins job.
# knife search results are getting collected in "D:\chef-workspace\node-names.txt" so that we can use this list of nodes after the execution as well. 



chef shell-init powershell | Invoke-Expression
cd $ENV:workspace | knife search "chef_environment:$ENV:Chef_Environment AND platform:$ENV:Platform AND tags:$ENV:Tag" -a name>D:\chef-workspace\node-names.txt
Try {
    $stdoutArray = Invoke-Expression "knife vault show credentials $ENV:vault_name -F json 2>&1"
    $stdoutString=[string]$stdoutArray
    #Convert to PSCustomObject
    $stdoutObject= $stdoutString | ConvertFrom-Json
    $winrm_username=$stdoutObject.username
    $winrm_password=$stdoutObject.password
}
Catch {
    echo $stdoutString
}
echo $winrm_username
$names = Get-Content D:\chef-workspace\node-names.txt | Select-String -Pattern "name"
Get-Content D:\chef-workspace\node-names.txt
$newLine=0
foreach ($line in $names) {  
    $line= $names[$newLine]
    $newLine=$newLine+1
    $array="$line".Split(":")
    $nodeName=$array[1]
    $nodeName = $nodeName -replace '\s',''
    start-job -Name $nodeName -scriptblock {
        param(
            $nodeName, $winrm_username, $winrm_password
        )
        $newPath = "D:\chef-workspace\Logs\"+"$nodeName"+".log"
        knife winrm --winrm-shell elevated $nodeName "chef-client -o $ENV:run_list -c c:/chef/client.rb" -m -x $winrm_username -P $winrm_password >$newPath
    } -ArgumentList $nodeName, $winrm_username, $winrm_password  
}

echo "2 mins delay. Waiting for chef-client to start executing."
sleep 120
    
C:\opscode\chefdk\embedded\bin\ruby.exe "$ENV:WORKSPACE\poll_patching_nodes.rb" "platform:$ENV:Platform AND tags:$ENV:Tag" "$ENV:Chef_Environment" "$ENV:WORKSPACE\.chef\knife.rb" "tesco_wsus_client" $ENV:Timeout