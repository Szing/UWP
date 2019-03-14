
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
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.DataTransfer;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : MVVMPage
    {

        public HomePage()
            : this(null)
        {

        }
        public HomePage(HomePage_Model model)
            : base(model)
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as HomePage_Model;
            });
            StrongTypeViewModel = this.ViewModel as HomePage_Model;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)

        {

            //创建和自定义 FileOpenPicker

            var picker = new Windows.Storage.Pickers.FileOpenPicker();

            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail; //可通过使用图片缩略图创建丰富的视觉显示，以显示文件选取器中的文件

            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".jpg");

            picker.FileTypeFilter.Add(".jpeg");

            picker.FileTypeFilter.Add(".png");

            picker.FileTypeFilter.Add(".gif");



            //选取单个文件

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();



            //文件处理

            if (file != null)

            {

                var inputFile = SharedStorageAccessManager.AddFile(file);

                var destination = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cropped.jpg", CreationCollisionOption.ReplaceExisting);//在应用文件夹中建立文件用来存储裁剪后的图像

                var destinationFile = SharedStorageAccessManager.AddFile(destination);

                var options = new LauncherOptions();

                options.TargetApplicationPackageFamilyName = "Microsoft.Windows.Photos_8wekyb3d8bbwe";



                //待会要传入的参数

                var parameters = new ValueSet();

                parameters.Add("InputToken", inputFile);                //输入文件

                parameters.Add("DestinationToken", destinationFile);    //输出文件

                parameters.Add("ShowCamera", false);                    //它允许我们显示一个按钮，以允许用户采取当场图象(但是好像并没有什么卵用)

                parameters.Add("EllipticalCrop", true);                 //截图区域显示为圆（最后截出来还是方形）

                parameters.Add("CropWidthPixals", 300);

                parameters.Add("CropHeightPixals", 300);



                //调用系统自带截图并返回结果

                var result = await Launcher.LaunchUriForResultsAsync(new Uri("microsoft.windows.photos.crop:"), options, parameters);



                //按理说下面这个判断应该没问题呀，但是如果裁剪界面点了取消的话后面会出现异常，所以后面我加了try catch

                if (result.Status == LaunchUriStatus.Success && result.Result != null)

                {

                    //对裁剪后图像的下一步处理

                    try

                    {

                        // 载入已保存的裁剪后图片

                        var stream = await destination.OpenReadAsync();

                        var bitmap = new BitmapImage();

                        await bitmap.SetSourceAsync(stream);



                        // 显示

                        Img.Source = bitmap;

                    }

                    catch (Exception ex)

                    {

                        Debug.WriteLine(ex.Message + ex.StackTrace);

                    }

                }

            }

        }
        public HomePage_Model StrongTypeViewModel
        {
            get { return (HomePage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(HomePage_Model), typeof(HomePage), new PropertyMetadata(null));




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
