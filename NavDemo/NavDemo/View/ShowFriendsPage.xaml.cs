
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
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowFriendPage : MVVMPage
    {

        public ShowFriendPage()
            : this(null)
        {
           
        }
        public ShowFriendPage(ShowFriendPage_Model model)
            : base(model)
        {
            
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as ShowFriendPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as ShowFriendPage_Model;
            
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
           
        }
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
           
        }



        public ShowFriendPage_Model StrongTypeViewModel
        {
            get { return (ShowFriendPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(ShowFriendPage_Model), typeof(ShowFriendPage), new PropertyMetadata(null));




        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }
        /// <summary>
        /// 显示确定取消菜单
        /// </summary>
        /// <param name="str">提示字段</param>
        private void ShowMessagePopupWindow(string str)
        {
            var msgPopup = new Resources.MessagePopupWindow(str);
            msgPopup.LeftClick += (s, e) => { StrongTypeViewModel.CommandDeleteFriend.Execute(null); };
            msgPopup.RightClick += (s, e) => { };
            msgPopup.ShowWIndow();
        }

        /// <summary>
        /// 删除好友事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowMessagePopupWindow("您确定要删除该好友吗");
        }
    }
}
