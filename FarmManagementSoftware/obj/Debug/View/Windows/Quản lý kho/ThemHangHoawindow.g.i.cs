﻿#pragma checksum "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4DA26977D4D948685C0DA1445FC58553029C2927D9D2B80A09D555642D72B75C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FarmManagementSoftware.View.Windows.Quản_lý_kho;
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


namespace FarmManagementSoftware.View.Windows.Quản_lý_kho {
    
    
    /// <summary>
    /// ThemHangHoawindow
    /// </summary>
    public partial class ThemHangHoawindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FarmManagementSoftware.View.Windows.Quản_lý_kho.ThemHangHoawindow ThemTTHangHoa;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMaHH;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTenHH;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDonGia;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSLTK;
        
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
            System.Uri resourceLocater = new System.Uri("/FarmManagementSoftware;component/view/windows/qu%e1%ba%a3n%20l%c3%bd%20kho/themh" +
                    "anghoawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
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
            this.ThemTTHangHoa = ((FarmManagementSoftware.View.Windows.Quản_lý_kho.ThemHangHoawindow)(target));
            return;
            case 2:
            this.tbMaHH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tbTenHH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbDonGia = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
            this.tbDonGia.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbSLTK = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\..\..\View\Windows\Quản lý kho\ThemHangHoawindow.xaml"
            this.tbSLTK.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

