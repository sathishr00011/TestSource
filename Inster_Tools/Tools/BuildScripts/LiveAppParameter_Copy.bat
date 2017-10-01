Set Source_PATH=%1
set Destination_PATH=\\uktee01-clusdb\IGHSBuildOutput\IGHS_Manifest

xcopy "%Source_PATH%\ApllicationPrameterfilePPEandLive\Restricted" "%Destination_PATH%\Restricted" /y /S /r /e
