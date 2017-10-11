param(
[string]$path
)
$u = New-Object System.Collections.ArrayList
$z=Get-ChildItem -Filter *.csproj -Path $path -Recurse | Select-Object -ExpandProperty FullName
[xml]$dllList=Get-Content NugetDlls.xml
foreach($file in $z)
{
    [xml]$y=Get-Content $file;
    $x=$y.Project.ItemGroup[0].reference | % {$_.Hintpath} | ?{$_ -ne $null} ;
    $w=$x | ForEach-Object{ $_.Split("\")[-1]};
    foreach($entry in $dllList.Packages.Package)
    {
        foreach($dll in $w)
        {
             if($entry.Dll -eq $dll)
             {
              $u+=$entry.name
             }
        }
    }
    $u|Select-Object -Unique | %{Install-Package -ProjectName $file.Tostring().Split("\")[-1].Replace(".csproj","") -id $_}
}
 
