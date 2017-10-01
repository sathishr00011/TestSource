$url = "http://pvcdljenma001uk.global.tesco.org/jenkins/job/IGHS/job/ChefDeployment_IGHS/job/Test1/buildWithParameters?token=TEST"

$ComponentName="RBC","CSC","Online","web","APP"
$lastjobstatus="true"

#&ComponentName=RBC$ISLastJob=true

#http://pvcdljenma001uk.global.tesco.org/jenkins/job/IGHS/job/ChefDeployment_IGHS/job/Test1/buildWithParameters?token=TEST&ComponentName=RBC$ISLastJob=true

$count=$ComponentName.length



foreach ($component in $ComponentName)
{

$count=$count-1

if ($count -eq 0)
{
$webrequesturl = $url+"&ComponentName=$component&ISLastJob=$lastjobstatus"
}
else
{
 $webrequesturl = $url+"&ComponentName=$component&ISLastJob=false"
}
$request = [System.Net.WebRequest]::Create( $webrequesturl)
$response = $request.GetResponse()
$response.Close()

}

