﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DisplaySettings.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DisplaySettings.Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The settings change was unsuccessful because system is DualView capable..
        /// </summary>
        internal static string DISP_CHANGE_BADDUALVIEW {
            get {
                return ResourceManager.GetString("DISP_CHANGE_BADDUALVIEW", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An invalid set of flags was passed in..
        /// </summary>
        internal static string DISP_CHANGE_BADFLAGS {
            get {
                return ResourceManager.GetString("DISP_CHANGE_BADFLAGS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An invalid parameter was passed in. This can include an invalid flag or combination of flags..
        /// </summary>
        internal static string DISP_CHANGE_BADPARAM {
            get {
                return ResourceManager.GetString("DISP_CHANGE_BADPARAM", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ChangeDisplaySettings API failed..
        /// </summary>
        internal static string DISP_CHANGE_FAILED {
            get {
                return ResourceManager.GetString("DISP_CHANGE_FAILED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to write settings to the registry..
        /// </summary>
        internal static string DISP_CHANGE_NOTUPDATED {
            get {
                return ResourceManager.GetString("DISP_CHANGE_NOTUPDATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown return value from ChangeDisplaySettings API..
        /// </summary>
        internal static string DISP_CHANGE_OTHER {
            get {
                return ResourceManager.GetString("DISP_CHANGE_OTHER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please restart your system..
        /// </summary>
        internal static string DISP_CHANGE_RESTART {
            get {
                return ResourceManager.GetString("DISP_CHANGE_RESTART", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Settings changed successfully..
        /// </summary>
        internal static string DISP_CHANGE_SUCCESSFUL {
            get {
                return ResourceManager.GetString("DISP_CHANGE_SUCCESSFUL", resourceCulture);
            }
        }
    }
}
