﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrackeysBot.Core.Resources {
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
    internal class EmbedMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmbedMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BrackeysBot.Core.Resources.EmbedMessages", typeof(EmbedMessages).Assembly);
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
        ///   Looks up a localized string similar to The core plugin cannot be disabled!.
        /// </summary>
        internal static string CantDisableCorePlugin {
            get {
                return ResourceManager.GetString("CantDisableCorePlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The core plugin cannot be unloaded!.
        /// </summary>
        internal static string CantUnloadCorePlugin {
            get {
                return ResourceManager.GetString("CantUnloadCorePlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was thrown when attempting to disable plugin &apos;{1}&apos;. See log for details..
        /// </summary>
        internal static string ErrorDisablingPlugin {
            get {
                return ResourceManager.GetString("ErrorDisablingPlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was thrown when attempting to enable plugin &apos;{1}&apos;. See log for details..
        /// </summary>
        internal static string ErrorEnablingPlugin {
            get {
                return ResourceManager.GetString("ErrorEnablingPlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} was thrown when attempting to load plugin &apos;{1}&apos;. See log for details..
        /// </summary>
        internal static string ErrorLoadingPlugin {
            get {
                return ResourceManager.GetString("ErrorLoadingPlugin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} {1} was successfully disabled..
        /// </summary>
        internal static string PluginDisabled {
            get {
                return ResourceManager.GetString("PluginDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} {1} was successfully enabled..
        /// </summary>
        internal static string PluginEnabled {
            get {
                return ResourceManager.GetString("PluginEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} {1} was successfully loaded and enabled..
        /// </summary>
        internal static string PluginLoaded {
            get {
                return ResourceManager.GetString("PluginLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No plugin with the name &apos;{0}&apos; is loaded!.
        /// </summary>
        internal static string PluginNotLoaded {
            get {
                return ResourceManager.GetString("PluginNotLoaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} {1} was successfully reloaded and enabled..
        /// </summary>
        internal static string PluginReloaded {
            get {
                return ResourceManager.GetString("PluginReloaded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} {1} was successfully unloaded..
        /// </summary>
        internal static string PluginUnloaded {
            get {
                return ResourceManager.GetString("PluginUnloaded", resourceCulture);
            }
        }
    }
}
