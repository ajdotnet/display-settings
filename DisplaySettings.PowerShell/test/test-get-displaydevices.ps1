
Import-Module ..\DisplaySettings.psd1
get-module display*

ECHO "*** Get-DisplayDevices"
Get-DisplayDevices

ECHO "*** Get-DisplayDevices | format-wide"
Get-DisplayDevices | format-wide

ECHO "*** Get-DisplayDevices | format-list"
Get-DisplayDevices | format-list

ECHO "*** Get-DisplayDevices | format-table"
Get-DisplayDevices | format-table



