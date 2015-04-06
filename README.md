# display-settings
Application and PowerShell plugin to query and set display settings (i.e. screen resolution).

This also serves as an example on how command line applications and PowerShell plugins compete in terms of features and development effort.

## Current feture set

### Command line 

The command line help should be self-explanatory:

```
Display Settings - (c) by Alexander Jung - https://github.com/ajdotnet/display-settings

SYNTAX:
    DisplaySettings.exe xres yres [color depth] [frequency]
    DisplaySettings.exe /query|/queryall

Systemparameter:

    @Parameterdatei     Arguments aus einer Datei einlesen
    !Logdatei           In eine Logdatei schreiben
    /?|/h[elp]          Hilfe anzeigen
    /v[erbose]          Ausführliche Ausgabe
    /q[uiet]            Keine Ausgabe
    /nologo             Kein Logo ausgeben


    xres                horizontal resolution
    yres                vertical resolution
    color depth         colors in bits (8, 16, 32)
    frequency           screen refresh rate
```

### PowerShell

Note: The blog post (see [background](#background) below) was written for Windows PowerShell 1.0. 
The code has since been updated to Windows PowerShell 2.0/4.0. I also changed it from
snapin to module (see [Modules and Snap-ins](https://msdn.microsoft.com/en-us/library/dd878246(v=vs.85).aspx)).

Details on how to install modules can be found [here](https://msdn.microsoft.com/en-us/library/dd878350(v=vs.85).aspx).
Essentially:
- You want to play with the code, use '.\load-module.ps1' in the output directory to load the module.
- You want to use the module "productively", you copy everything to '%userprofile%\Documents\WindowsPowershell\Modules\DisplaySettings\'.
Load the module using 'Import-Module DisplaySettings'.


## background

For background see https://ajdotnet.wordpress.com/2008/01/19/command-line-tool-vs-powershell-cmdlet/

Note: This solution depends on https://github.com/ajdotnet/AJ.Common and https://github.com/ajdotnet/AJ.Console.

