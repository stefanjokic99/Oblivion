﻿#pragma checksum "..\..\UnosDVDWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "084BA2FFF80CA905E74B3E701D630FF82723CF05F24DDA2606D229A8DEAA0FE1"
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
using Oblivion_Prototip;
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


namespace Oblivion_Prototip {
    
    
    /// <summary>
    /// UnosDVDWindow
    /// </summary>
    public partial class UnosDVDWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblNaslov;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbJibKomponente;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNazivProizvodjaca;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbBrzina;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOdustani;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPotvrda;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\UnosDVDWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbBtnUnos;
        
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
            System.Uri resourceLocater = new System.Uri("/Oblivion-Prototip;component/unosdvdwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UnosDVDWindow.xaml"
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
            this.lblNaslov = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.tbJibKomponente = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\UnosDVDWindow.xaml"
            this.tbJibKomponente.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tbJibKomponente_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNazivProizvodjaca = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbBrzina = ((System.Windows.Controls.TextBox)(target));
            
            #line 61 "..\..\UnosDVDWindow.xaml"
            this.tbBrzina.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tbBrzina_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnOdustani = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\UnosDVDWindow.xaml"
            this.btnOdustani.Click += new System.Windows.RoutedEventHandler(this.btnOdustani_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnPotvrda = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\UnosDVDWindow.xaml"
            this.btnPotvrda.Click += new System.Windows.RoutedEventHandler(this.btnPotvrda_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tbBtnUnos = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
