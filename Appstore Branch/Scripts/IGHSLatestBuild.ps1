$tf = &"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\tf.exe"     get $/TescoAppStore/Cell46Main/Tesco /recursive
write-output $tf
$reslove =&"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\tf.exe" resolve /auto:OverwriteLocal
PowerShell.exe -noexit .\IGHS.ps1
