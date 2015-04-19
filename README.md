# display-settings
Application and PowerShell plugin to query and set display settings (i.e. screen resolution).

This also serves as an example on how command line applications and PowerShell plugins compete in terms of features and development effort.

## Current feature set

The current feature set is as follows:
- get current and available resolutions
- set current resolution
- get and address available screens for multi-monitor configurations
- localizations for en-US and de-DE.

### Command line 

The command line help should be rather self-explanatory:

```
> DisplaySettings.exe /?
Display Settings - (c) by Alexander Jung - https://github.com/ajdotnet/display-settings

SYNTAX:
    DisplaySettings.exe xres yres [color depth] [frequency]
    DisplaySettings.exe /query|/queryAll [/device device]
    DisplaySettings.exe /queryDevices

System parameter:

    @parameterfile      read arguments from file
    !logfile            write output also to log file
    /?|/h[help]         show help
    /v[erbose]          show verbose output
    /q[uiet]            show no output
    /nologo             show no logo


    xres                horizontal resolution
    yres                vertical resolution
    color depth         colors in bits (8, 16, 32)
    frequency           screen refresh rate
    device              device number, as reported by /queryDevices
```

The `queryDevices` switch comes into play on multi-monitor configurations:

```
> DisplaySettings.exe /querydevices
Display Settings - (c) by Alexander Jung - https://github.com/ajdotnet/display-settings

Display Adapters

    # Adapter                      Monitor
    = =========================    ===================================
    1 Intel(R) HD Graphics 4000 -> ThinkPad MaxBright Display 1600x900
    2 Intel(R) HD Graphics 4000 -> Generic PnP Monitor                 (primary)
```

The device number then works on the `query` switch:

```
> DisplaySettings.exe /query /device 1
Display Settings - (c) by Alexander Jung - https://github.com/ajdotnet/display-settings

Display Settings

	Monitor:        #1: ThinkPad MaxBright Display 1600x900
    Resolution:     1600 x 900
    Color Depth:    32 bits per pixel
    Refresh Rate:   60 Hertz
```


### PowerShell

Note: The blog post (see [background](#background) below) was written for Windows PowerShell 1.0. 
The code has since been updated to Windows PowerShell 2.0/4.0. I also changed it from
snapin to module (see [Modules and Snap-ins](https://msdn.microsoft.com/en-us/library/dd878246(v=vs.85).aspx)).

Details on how to install modules can be found [here](https://msdn.microsoft.com/en-us/library/dd878350(v=vs.85).aspx).
Essentially:
- You want to play with the code, use `.\load-module.ps1` in the output directory to load the module.
- You want to use the module "productively", you copy everything to `%userprofile%\Documents\WindowsPowershell\Modules\DisplaySettings\`.
Load the module using `Import-Module DisplaySettings`.

Additionally I have added multi-monitor support and proper localizations for en-US and de-DE.

## background

For background see https://ajdotnet.wordpress.com/2008/01/19/command-line-tool-vs-powershell-cmdlet/

Note: This solution depends on [AJ.Common](https://github.com/ajdotnet/AJ.Common) and [AJ.Console](https://github.com/ajdotnet/AJ.Console).

