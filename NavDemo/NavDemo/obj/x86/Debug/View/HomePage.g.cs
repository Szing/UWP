﻿#pragma checksum "D:\UWP-master\NavDemo\NavDemo\View\HomePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "883BB8E043AA7CAA59B21168EF4BCA30"
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // View\HomePage.xaml line 1
                {
                    this.pageRoot = (global::MVVMSidekick.Views.MVVMPage)(target);
                }
                break;
            case 2: // View\HomePage.xaml line 39
                {
                    this.detailGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // View\HomePage.xaml line 63
                {
                    this.btnHide = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnHide).Click += this.BtnHide_Click;
                }
                break;
            case 4: // View\HomePage.xaml line 215
                {
                    this.btnHide1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnHide1).Click += this.BtnHide1_Click;
                }
                break;
            case 5: // View\HomePage.xaml line 222
                {
                    this.detailGrid1 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // View\HomePage.xaml line 227
                {
                    this.MyASBox = (global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target);
                }
                break;
            case 7: // View\HomePage.xaml line 76
                {
                    this.openFileButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.openFileButton).Click += this.OpenButton_Click;
                }
                break;
            case 8: // View\HomePage.xaml line 78
                {
                    this.saveFileButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.saveFileButton).Click += this.SaveButton_Click;
                }
                break;
            case 9: // View\HomePage.xaml line 81
                {
                    this.imageButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.imageButton).Click += this.ImageButton_Click;
                }
                break;
            case 10: // View\HomePage.xaml line 83
                {
                    this.Font = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 11: // View\HomePage.xaml line 94
                {
                    this.fontColorButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 12: // View\HomePage.xaml line 164
                {
                    this.FontSize = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 13: // View\HomePage.xaml line 176
                {
                    this.boldbutton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.boldbutton).Click += this.BoldButton_Click;
                }
                break;
            case 14: // View\HomePage.xaml line 178
                {
                    this.italicButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.italicButton).Click += this.ItalicButton_Click;
                }
                break;
            case 15: // View\HomePage.xaml line 180
                {
                    this.underlineButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.underlineButton).Click += this.UnderlineButton_Click;
                }
                break;
            case 16: // View\HomePage.xaml line 183
                {
                    this.editor = (global::Windows.UI.Xaml.Controls.RichEditBox)(target);
                    ((global::Windows.UI.Xaml.Controls.RichEditBox)this.editor).ContextMenuOpening += this.Editor_ContextMenuOpening;
                }
                break;
            case 17: // View\HomePage.xaml line 190
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element17 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element17).Click += this.OnCopy;
                }
                break;
            case 18: // View\HomePage.xaml line 191
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element18 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element18).Click += this.OnCut;
                }
                break;
            case 19: // View\HomePage.xaml line 192
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element19 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element19).Click += this.OnPaste;
                }
                break;
            case 20: // View\HomePage.xaml line 202
                {
                    global::Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem element20 = (global::Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.ToggleMenuFlyoutItem)element20).Click += this.BoldButton_Click;
                }
                break;
            case 21: // View\HomePage.xaml line 205
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element21 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element21).Click += this.OnUnderline;
                }
                break;
            case 22: // View\HomePage.xaml line 206
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element22 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element22).Click += this.OnUnderline;
                }
                break;
            case 23: // View\HomePage.xaml line 207
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element23 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element23).Click += this.OnUnderline;
                }
                break;
            case 24: // View\HomePage.xaml line 208
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element24 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element24).Click += this.OnUnderline;
                }
                break;
            case 25: // View\HomePage.xaml line 195
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element25 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element25).Click += this.FontSizeButton_Click;
                }
                break;
            case 26: // View\HomePage.xaml line 196
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element26 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element26).Click += this.FontSizeButton_Click;
                }
                break;
            case 27: // View\HomePage.xaml line 197
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element27 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element27).Click += this.FontSizeButton_Click;
                }
                break;
            case 28: // View\HomePage.xaml line 198
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element28 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element28).Click += this.FontSizeButton_Click;
                }
                break;
            case 29: // View\HomePage.xaml line 199
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element29 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element29).Click += this.FontSizeButton_Click;
                }
                break;
            case 30: // View\HomePage.xaml line 168
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element30 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element30).Click += this.FontSizeButton_Click;
                }
                break;
            case 31: // View\HomePage.xaml line 169
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element31 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element31).Click += this.FontSizeButton_Click;
                }
                break;
            case 32: // View\HomePage.xaml line 170
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element32 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element32).Click += this.FontSizeButton_Click;
                }
                break;
            case 33: // View\HomePage.xaml line 171
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element33 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element33).Click += this.FontSizeButton_Click;
                }
                break;
            case 34: // View\HomePage.xaml line 172
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element34 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element34).Click += this.FontSizeButton_Click;
                }
                break;
            case 35: // View\HomePage.xaml line 114
                {
                    global::Windows.UI.Xaml.Controls.Button element35 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element35).Click += this.ColorButton_Click;
                }
                break;
            case 36: // View\HomePage.xaml line 119
                {
                    global::Windows.UI.Xaml.Controls.Button element36 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element36).Click += this.ColorButton_Click;
                }
                break;
            case 37: // View\HomePage.xaml line 124
                {
                    global::Windows.UI.Xaml.Controls.Button element37 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element37).Click += this.ColorButton_Click;
                }
                break;
            case 38: // View\HomePage.xaml line 129
                {
                    global::Windows.UI.Xaml.Controls.Button element38 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element38).Click += this.ColorButton_Click;
                }
                break;
            case 39: // View\HomePage.xaml line 134
                {
                    global::Windows.UI.Xaml.Controls.Button element39 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element39).Click += this.ColorButton_Click;
                }
                break;
            case 40: // View\HomePage.xaml line 139
                {
                    global::Windows.UI.Xaml.Controls.Button element40 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element40).Click += this.ColorButton_Click;
                }
                break;
            case 41: // View\HomePage.xaml line 144
                {
                    global::Windows.UI.Xaml.Controls.Button element41 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element41).Click += this.ColorButton_Click;
                }
                break;
            case 42: // View\HomePage.xaml line 149
                {
                    global::Windows.UI.Xaml.Controls.Button element42 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element42).Click += this.ColorButton_Click;
                }
                break;
            case 43: // View\HomePage.xaml line 154
                {
                    global::Windows.UI.Xaml.Controls.Button element43 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element43).Click += this.ColorButton_Click;
                }
                break;
            case 44: // View\HomePage.xaml line 86
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element44 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element44).Click += this.FontButton_Click;
                }
                break;
            case 45: // View\HomePage.xaml line 87
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element45 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element45).Click += this.FontButton_Click;
                }
                break;
            case 46: // View\HomePage.xaml line 88
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element46 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element46).Click += this.FontButton_Click;
                }
                break;
            case 47: // View\HomePage.xaml line 89
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element47 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element47).Click += this.FontButton_Click;
                }
                break;
            case 48: // View\HomePage.xaml line 90
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element48 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element48).Click += this.FontButton_Click;
                }
                break;
            case 49: // View\HomePage.xaml line 44
                {
                    this.MemoryCalenderDatePicker = (global::Windows.UI.Xaml.Controls.CalendarDatePicker)(target);
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

