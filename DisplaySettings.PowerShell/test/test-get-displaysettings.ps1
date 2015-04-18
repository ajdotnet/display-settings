
Import-Module ..\DisplaySettings.psd1
get-module display*

Get-DisplaySettings 
Get-DisplaySettings -device 1
Get-DisplaySettings -device 2
Get-DisplaySettings -device 3

Get-DisplaySettings -device 1 | format-wide
Get-DisplaySettings -device 1 | format-list
Get-DisplaySettings -device 1 | format-table


