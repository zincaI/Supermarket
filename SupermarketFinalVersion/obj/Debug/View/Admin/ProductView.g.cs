﻿#pragma checksum "..\..\..\..\View\Admin\ProductView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "954C6722884C941A615AED303B9748DFD67D2F2954C94ADFD3036A2D2C919908"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SupermarketFinalVersion.Converters;
using SupermarketFinalVersion.View.Admin;
using SupermarketFinalVersion.ViewModel;
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


namespace SupermarketFinalVersion.View.Admin {
    
    
    /// <summary>
    /// ProductView
    /// </summary>
    public partial class ProductView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid productGrid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox productname;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox producername;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox categoryname;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox barcode;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button modifyButton;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\View\Admin\ProductView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteButton;
        
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
            System.Uri resourceLocater = new System.Uri("/SupermarketFinalVersion;component/view/admin/productview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Admin\ProductView.xaml"
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
            this.productGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.productname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.producername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.categoryname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.barcode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.modifyButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.deleteButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
