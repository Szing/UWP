﻿#pragma checksum "D:\UWP-master\NavDemo\NavDemo\HomePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "590CCB20702379780140F29EB543BB41"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NavDemo
{
    partial class HomePage : 
        global::MVVMSidekick.Views.MVVMPage, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.pageRoot = (global::MVVMSidekick.Views.MVVMPage)(target);
                }
                break;
            case 2:
                {
                    this.pageTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.MyASBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 4:
                {
                    this.ImageButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 264 "..\..\..\HomePage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.ImageButton).Click += this.Button_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.Img = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 6:
                {
                    this.openFileButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 7:
                {
                    this.imageButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 8:
                {
                    this.Font = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 9:
                {
                    this.fontColorButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 10:
                {
                    this.FontSize = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 11:
                {
                    this.boldbutton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 12:
                {
                    this.italicButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 13:
                {
                    this.underlineButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 14:
                {
                    this.editor = (global::Windows.UI.Xaml.Controls.RichEditBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

