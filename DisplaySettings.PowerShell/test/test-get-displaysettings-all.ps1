
Import-Module ..\DisplaySettings.psd1
get-module display*

Get-DisplaySettings -all | format-table
Get-DisplaySettings -all -device 1 | format-wide
Get-DisplaySettings -all -device 2 | format-list

