
using NavDemo.ViewModels;
using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Text;
using NavDemo.AttachProps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : MVVMPage
    {
        enum CommandKind{ Save =0 ,Delete = 1 } ;

        public AboutPage()
            : this(null)
        {

        }
        public AboutPage(AboutPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                
                StrongTypeViewModel = this.ViewModel as AboutPage_Model;
                
            });
            StrongTypeViewModel = this.ViewModel as AboutPage_Model;
           
        }


        public AboutPage_Model StrongTypeViewModel
        {
            get { return (AboutPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(AboutPage_Model), typeof(AboutPage), new PropertyMetadata(null));




        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        
        string old = "";
        
        /// <summary>
        /// 保存文档事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string str = "";
            editor.Document.GetText(TextGetOptions.FormatRtf, out str);
            if (str != old)
            {
                RtfText.SetRichText(editor, str);
                old = str;
            }
            ShowMessagePopupWindow("您确定要更改该文档吗", CommandKind.Save);
        }

        /// <summary>
        /// 删除日志事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMessagePopupWindow("您确定要删除该日志吗", CommandKind.Delete);
        }

        /// <summary>
        /// 显示确定取消菜单
        /// </summary>
        /// <param name="str">提示字段</param>
        /// <param name="mode">确定后运行的模式</param>
        private void ShowMessagePopupWindow(string str,CommandKind mode)
        {
            var msgPopup = new Resources.MessagePopupWindow(str);
            switch(mode)
            {
                case CommandKind.Save:
                    msgPopup.LeftClick += (s, e) => { StrongTypeViewModel.CommandSaveDialog.Execute(null); };
                    break;
                case CommandKind.Delete:
                    msgPopup.LeftClick += (s, e) => { StrongTypeViewModel.CommandDeleteDialog.Execute(null); };
                    break;
            }
            
            msgPopup.RightClick += (s, e) => { };
            msgPopup.ShowWIndow();
        }
    }
}
