
Import-Module ..\DisplaySettings.psd1
get-module display*

ECHO "*** Get-DisplaySettings "
Get-DisplaySettings 

ECHO "*** Get-DisplaySettings -device 1"
Get-DisplaySettings -device 1

ECHO "*** Get-DisplaySettings -device 2"
Get-DisplaySettings -device 2

ECHO "*** Get-DisplaySettings -device 3"
Get-DisplaySettings -device 3

ECHO "*** Get-DisplaySettings -device 1 | format-wide"
Get-DisplaySettings -device 1 | format-wide

ECHO "*** Get-DisplaySettings -device 1 | format-list"
Get-DisplaySettings -device 1 | format-list

ECHO "*** Get-DisplaySettings -device 1 | format-table"
Get-DisplaySettings -device 1 | format-table


