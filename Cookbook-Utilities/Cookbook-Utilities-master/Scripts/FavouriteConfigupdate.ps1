$Projectconfigpath=$env:Projectconfigpath
$Projectconfigpath=($Projectconfigpath+"\ce\Web_ce.config"),($Projectconfigpath+"\ap\Web_ap.config")

foreach($WebProjectpath in $Projectconfigpath)
{
$Applicationparameterfile=$env:Appfile

$cookbooklocation=$env:cookbook

$webconfigfile_ce=$cookbooklocation+"\templates\dev\ce\Web.config.erb"
$webconfigfile_ap=$cookbooklocation+"\templates\dev\ap\Web.config.erb"



$Applicationparameterfiles= ($Applicationparameterfile+"\NonRestricted\DEV\CE\23.0\Manifest_IGHS_UK_CE_DEV_Application_Parameters.xml"),($Applicationparameterfile+"\NonRestricted\DEV\AP\23.0\Manifest_IGHS_UK_AP_DEV_Application_Parameters.xml")

foreach($Applicationparameterfile in $Applicationparameterfiles)
{

$doc = new-object System.Xml.XmlDocument
$doc.Load($Applicationparameterfile)
Set-ItemProperty $WebProjectpath -name IsReadOnly -value $false

##
$config=new-object System.Xml.XmlDocument
$config.Load($WebProjectpath)
##



## Application prameter file ##
$nodelist=$doc.SelectNodes('/Application/Environment/Services/Service[@name="AppStore.Favourite.Service"]/Web.Config/LoggingConfiguration/Listeners')
##

## webconfig config file ##
$confignodelist=$config.SelectNodes("/configuration/loggingConfiguration/listeners")
##

$nodelist | % {$_.add} | % { 
 $nodename = $_.name
 if($nodename -eq "Rolling Flat File Trace Listener")
 { 
  $filename = $_.filename
  $listervalue=$_.value
    $confignodelist | % { $_.add } | % { 
        if($nodename -eq $_.name)
        {    
          $_.filename = $filename
          $_.formatter= $listervalue           
        }    
    }
 }
 elseif($nodename -eq "Event Log Trace Listener")
 {
  $formatter = $_.value
    $confignodelist | % { $_.add } | % { 
        if($nodename -eq $_.name)
        {    
          $_.formatter = $formatter           
        }    
    }
 }
 
}
$config.save($WebProjectpath)


## Application prameter file ##
$nodelist1=$doc.SelectNodes('/Application/Environment/Services/Service[@name="AppStore.Favourite.Service"]/Web.Config/LoggingConfiguration/CategorySources')
##

## webconfig config file ##
$confignodelist1=$config.SelectNodes("/configuration/loggingConfiguration/categorySources")
##

$nodelist1 | % {$_.add } | % {

$nodename1=$_.Name

    if ($nodename1 -eq "TescoFavoriteApp")
    {
    $value=$_.value
    
    $confignodelist1 | %{$_.add } | % {
    
        if ($nodename1 -eq $_.name)
        {
        $_.switchValue=$value
        }
    }
    
    }

}

$config.save($WebProjectpath)

## Application prameter file ##
$nodelist2=$doc.SelectNodes('/Application/Environment/Services/Service[@name="AppStore.Favourite.Service"]/Web.Config/appSettings')
##

## webconfig config file ##
$confignodelist2=$config.SelectNodes("/configuration/appSettings")
##

$nodelist2 | % {$_.add } | % {

$nodename2=$_.key
$nodevalue2=$_.value

$confignodelist2 | % {$_.add } | % {

    if ($nodename2 -eq $_.key )
    {
       $_.value=$nodevalue2 
    }
}

}

$config.save($WebProjectpath)

$confignodelist3=$config.SelectNodes("/configuration/connectionStrings")

$doc.Application.Environment.Services.Service | % { 
        if ($_.name -eq "AppStore.Favourite.Service")
        { 
         $_.DBConnection | % {
         $namedb=$_.name 
         $conn_password= $_.Account.Authentication.Password.value
         $conn_userid =  $_.Account.Authentication.UserId.value
         $conn_catalog = $_.Account.InitialCatalog.value
         $conn_source = $_.Account.DataSource.value       
            $confignodelist3 | % {$_.add} | % {
          if($namedb -eq $_.name)
          {
           $_.connectionString = "Password=$conn_password;Persist Security Info=False;User ID=$conn_userid;Initial Catalog=$conn_catalog;Data Source=$conn_source"
          }
          }
         }
      }
      }


$config.save($WebProjectpath)

if($Applicationparameterfile -match "Manifest_IGHS_UK_CE_DEV_Application_Parameters.xml" -and $WebProjectpath -match "Web_ce.config")
{ 
    copy-Item $WebProjectpath -Destination "$webconfigfile_ce" -Force
}
elseif($Applicationparameterfile -match "Manifest_IGHS_UK_AP_DEV_Application_Parameters.xml" -and $WebProjectpath -match "Web_ap.config")
{ 
    copy-Item $WebProjectpath -Destination "$webconfigfile_ap" -Force
}

}
}
