﻿#pragma checksum "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C6DEF52FEC737458974204E2C4483610A4E05D9F6C7623A3D5E814A96774801D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp_MVVM.View.Windows;


namespace FarmManagementSoftware.View.Windows.Quản_lý_kho {
    
    
    /// <summary>
    /// ChonHangHoaWindow
    /// </summary>
    public partial class ChonHangHoaWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FarmManagementSoftware.View.Windows.Quản_lý_kho.ChonHangHoaWindow ChonHangHoaW;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTenHH;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbb_LoaiPhieu;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSLHH;
        
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
            System.Uri resourceLocater = new System.Uri("/FarmManagementSoftware;component/view/windows/qu%e1%ba%a3n%20l%c3%bd%20kho/chonh" +
                    "anghoawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
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
            this.ChonHangHoaW = ((FarmManagementSoftware.View.Windows.Quản_lý_kho.ChonHangHoaWindow)(target));
            return;
            case 2:
            this.txtTenHH = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cbb_LoaiPhieu = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.txtSLHH = ((System.Windows.Controls.TextBox)(target));
            
            #line 92 "..\..\..\..\..\View\Windows\Quản lý kho\ChonHangHoaWindow.xaml"
            this.txtSLHH.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

