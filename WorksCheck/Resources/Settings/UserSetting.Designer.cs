﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorksCheck.Resources.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.10.0.0")]
    internal sealed partial class UserSetting : global::System.Configuration.ApplicationSettingsBase {
        
        private static UserSetting defaultInstance = ((UserSetting)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new UserSetting())));
        
        public static UserSetting Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<WorksCheck.Scripts.HomeworkData> Plan {
            get {
                return ((global::System.Collections.Generic.List<WorksCheck.Scripts.HomeworkData>)(this["Plan"]));
            }
            set {
                this["Plan"] = value;
            }
        }
    }
}
