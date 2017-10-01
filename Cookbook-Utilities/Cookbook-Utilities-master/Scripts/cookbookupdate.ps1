#Full path names

#$manifestfile="D:\ChefManifest\ComponentManifest_IGHS_AppStore_UK_Authentication_12.0.0.2.xml"
#$cookbookattributefile="D:\ChefManifest\install.rb"

#Parameter values

$manifestfile=$env:Componentmanifest_Path
$cookbookattributefile=$env:Cookbook_Path

$manifet= [xml](gc $manifestfile)
$attributes=gc($cookbookattributefile)
$manifestnodes = $manifet.SelectNodes("//Package")

foreach($manifestnode in $manifestnodes)
{
 # last . after that add \\ and find decimal to replace       
$sr=$manifestnode.path | % { $_ -replace "^.*\\"  -replace ".\d.*$"}

        $attributes | % {

                            if($_ -match 'download_path')
                                {
                                   # $_ -replace "^.*/" -replace ".\d.*$" 

                                        if ($_ -match $sr)
                                                {
                                                  $msiname = $manifestnode.path -replace "^.*\\" -replace ".\d.*$" 
                                                    $_ -replace "D:/Deployments.*msi","D:/Deployments/$msiname.$($manifestnode.version).msi"

                                                }
                                          else{$_}
                                                
                                  }              
                              elseif($_ -match 'dfspath')
                              {
                                         if ($_ -match $sr)
                                         {
                                                    $_ -replace "\\.*msi",$manifestnode.path -replace "\\","\\"
                                         }
                                         else{$_}
                                        
                              }              
                              else
                              {
                              $_
                              }  
                                

                        }| Set-Variable attributes
}

$attributes | Set-Content $cookbookattributefile
    
    
    
      
