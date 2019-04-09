
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
    public sealed partial class AddFriendPage : MVVMPage
    {

        public AddFriendPage()
            : this(null)
        {

        }
        public AddFriendPage(AddFriendPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as AddFriendPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as AddFriendPage_Model;
        }

        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            var defer = e.GetDeferral();

            try
            {
                DataPackageView dataView = e.DataView;
                // 拖放类型为文件存储。
                if (dataView.Contains(StandardDataFormats.StorageItems))
                {
                    var files = await dataView.GetStorageItemsAsync();
                    StorageFile file = files.OfType<StorageFile>().First();
                    
                    //
                    if (file.FileType == ".png" || file.FileType == ".jpg" || file.FileType == ".jpeg" || file.FileType == ".gif")
                    {
                        // 拖放的是图片文件。

                        
                        string constpath = "D:\\UWP-master\\NavDemo\\NavDemo\\" ;
                        int flag = 1;
                        for (int i = 0; i < constpath.Length; ++i )
                        {
                            
                            if(file.Path[i] != constpath[i])
                            {
                                flag = 0;
                                break;
                            }
                        }
                        if(flag == 0 )
                        {
                            await new Windows.UI.Popups.MessageDialog("请选择在工程文件夹下的图片，否则无效").ShowAsync();
                            return;
                        }
                        else
                        {
                            StrongTypeViewModel.friend.iconFriend = "ms-appx:///" + file.Path.Substring(constpath.Length);
                        }
                        BitmapImage bitmap = new BitmapImage();
                        await bitmap.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                        imageBrushFriend.ImageSource = bitmap;
                    }
                }
            }
            finally
            {
                defer.Complete();
            }
        }
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            //需要using Windows.ApplicationModel.DataTransfer;
            e.AcceptedOperation = DataPackageOperation.Copy;

            // 设置拖放时显示的文字。
            //e.DragUIOverride.Caption = "拖放打开";

            // 是否显示拖放时的文字。默认为 true。
            //e.DragUIOverride.IsCaptionVisible = false;

            // 是否显示文件预览内容，一般为文件图标。默认为 true。
            // e.DragUIOverride.IsContentVisible = false;

            // Caption 前面的图标是否显示。默认为 true。
            //e.DragUIOverride.IsGlyphVisible = false;

            //需要using Windows.UI.Xaml.Media.Imaging;
            //设置拖动图形，覆盖文件预览
            //e.DragUIOverride.SetContentFromBitmapImage(new BitmapImage(new Uri("ms-appx:///Assets/1.jpg")));

            e.Handled = true;
        }



        public AddFriendPage_Model StrongTypeViewModel
        {
            get { return (AddFriendPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(AddFriendPage_Model), typeof(AddFriendPage), new PropertyMetadata(null));




        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

       

    }
}
