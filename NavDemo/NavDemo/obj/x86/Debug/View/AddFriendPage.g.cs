﻿#pragma checksum "D:\UWP-master\NavDemo\NavDemo\View\AddFriendPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E85875910458959B8A71DBCE70465830"
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
    partial class AddFriendPage : 
        global::MVVMSidekick.Views.MVVMPage, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // View\AddFriendPage.xaml line 1
                {
                    this.pageRoot = (global::MVVMSidekick.Views.MVVMPage)(target);
                }
                break;
            case 2: // View\AddFriendPage.xaml line 21
                {
                    global::Windows.UI.Xaml.Controls.Grid element2 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    ((global::Windows.UI.Xaml.Controls.Grid)element2).DragOver += this.Grid_DragOver;
                    ((global::Windows.UI.Xaml.Controls.Grid)element2).Drop += this.Grid_Drop;
                }
                break;
            case 3: // View\AddFriendPage.xaml line 118
                {
                    this.kkk = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 4: // View\AddFriendPage.xaml line 124
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.InsertButton_Click ;
                }
                break;
            case 5: // View\AddFriendPage.xaml line 120
                {
                    this.insertFriendButton = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
                }
                break;
            case 6: // View\AddFriendPage.xaml line 76
                {
                    this.imageBrushFriend = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

