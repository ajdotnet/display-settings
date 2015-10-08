
Import-Module ..\DisplaySettings.psd1
get-module display*

ECHO "*** Get-DisplaySettings -all | format-table"
Get-DisplaySettings -all | format-table

ECHO "*** Get-DisplaySettings -all -device 1 | format-wide"
Get-DisplaySettings -all -device 1 | format-wide

ECHO "*** Get-DisplaySettings -all -device 2 | format-list"
Get-DisplaySettings -all -device 2 | format-list

