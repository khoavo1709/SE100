﻿#pragma checksum "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "888F6ACC685BB0505D5E9B4175B669BB3F8C2E93A6F92A700323A2BECBD248BB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FarmManagementSoftware.View.Windows;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FarmManagementSoftware.View.Windows.Quản_lý_loại_heo {
    
    
    /// <summary>
    /// Quanlyloaiheo
    /// </summary>
    public partial class Quanlyloaiheo : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Pigname_textbox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Mota_textbox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Find_textbox;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Find_button;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listviewHeo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FarmManagementSoftware;component/view/windows/qu%e1%ba%a3n%20l%c3%bd%20lo%e1%ba%" +
                    "a1i%20heo/quanlyloaiheo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Pigname_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Mota_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 63 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_ThemClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Find_textbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 86 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            this.Find_textbox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Find_textbox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Find_button = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            this.Find_button.Click += new System.Windows.RoutedEventHandler(this.Find_button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.listviewHeo = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            
            #line 137 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnFix_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 141 "..\..\..\..\..\View\Windows\Quản lý loại heo\Quanlyloaiheo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

