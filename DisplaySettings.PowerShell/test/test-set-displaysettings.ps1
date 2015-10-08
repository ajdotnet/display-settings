
Import-Module ..\DisplaySettings.psd1
get-module display*

ECHO "*** Set-DisplaySettings 1024 768 -device 1 "
Set-DisplaySettings 1024 768 -device 1 

ECHO "*** Set-DisplaySettings 1600 900 -device 1 "
Set-DisplaySettings 1600 900 -device 1 
