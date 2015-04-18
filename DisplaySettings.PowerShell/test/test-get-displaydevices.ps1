
Import-Module ..\DisplaySettings.psd1
get-module display*

Get-DisplayDevices
Get-DisplayDevices | format-wide
Get-DisplayDevices | format-list
Get-DisplayDevices | format-table



