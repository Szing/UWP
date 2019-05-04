
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
using NavDemo.Services;
using Windows.Storage.Streams;
using Windows.Phone.UI.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Text;
using NavDemo.AttachProps;


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

        
        /// <summary>
        /// 打开并导入rtf文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Streams.IRandomAccessStream randAccStream =
               await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream();

            if (randAccStream != null)
            {
                // Load the file into the Document property of the RichEditBox.
                editor.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
            }
            
        }
        /// <summary>
        /// 导出rtf文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ServiceLocator.Instance.Resolve<FileService>().SetRandomAccessStream(editor);

        }
        /// <summary>
        /// 更改字体样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            var font = item.FontFamily;
            editor.FontFamily = font;
        }
        /// <summary>
        /// 更改字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            // Extract the color of the button that was clicked.
            Button clickedColor = (Button)sender;
            var rectangle = (Windows.UI.Xaml.Shapes.Rectangle)clickedColor.Content;
            var color = ((Windows.UI.Xaml.Media.SolidColorBrush)rectangle.Fill).Color;
            editor.Document.Selection.CharacterFormat.ForegroundColor = color;

            fontColorButton.Flyout.Hide();
            editor.Focus(Windows.UI.Xaml.FocusState.Keyboard);
        }
        /// <summary>
        /// 更改字体大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            // 获取字号
            float size = Convert.ToSingle(item.Tag);

            editor.Document.Selection.CharacterFormat.Size = size;
        }
        /// <summary>
        /// 加粗选中文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }
        /// <summary>
        /// 使选中文字变成斜体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = Windows.UI.Text.FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }
        /// <summary>
        /// 为选中区域添加下划线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.Text.ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                if (charFormatting.Underline == Windows.UI.Text.UnderlineType.None)
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.Single;
                }
                else
                {
                    charFormatting.Underline = Windows.UI.Text.UnderlineType.None;
                }
                selectedText.CharacterFormat = charFormatting;
            }
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void   ImageButton_Click(object sender, RoutedEventArgs e)
        {
           
            var stream = await ServiceLocator.Instance.Resolve<ImageService>().GetImageStream();
            editor.Document.Selection.InsertImage(200, 100, 0, Windows.UI.Text.VerticalCharacterAlignment.Baseline, "图像", stream);
            

        }

        /// <summary>
        /// 向上隐藏区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            if (detailGrid.Visibility == Visibility.Collapsed)
            {
                btnHide.Content = "︽点击看看";
                detailGrid.Visibility = Visibility.Visible;

            }
            else
            {
                btnHide.Content = "︾隐藏之后";

                detailGrid.Visibility = Visibility.Collapsed;

            }


        }
        /// <summary>
        /// 向下隐藏区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHide1_Click(object sender, RoutedEventArgs e)
        {
            if (detailGrid1.Visibility == Visibility.Collapsed)
            {
                btnHide1.Content = "︾点击看看";
                detailGrid1.Visibility = Visibility.Visible;

            }
            else
            {
                btnHide1.Content = "︽隐藏之后";

                detailGrid1.Visibility = Visibility.Collapsed;

            }


        }
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Editor_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            MenuFlyout menu = FlyoutBase.GetAttachedFlyout(editor) as MenuFlyout;
            menu?.ShowAt(editor, new Point(e.CursorLeft, e.CursorTop));
        }
        /// <summary>
        /// 复制功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCopy(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.Copy();
        }
        /// <summary>
        /// 剪切功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCut(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.Cut();
        }
        /// <summary>
        /// 黏贴功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaste(object sender, RoutedEventArgs e)
        {
            // Paste方法带有一个整型参数，表示要粘贴的格式
            editor.Document.Selection.Paste(0);
        }
        /// <summary>
        /// 添加下划线功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUnderline(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            int x = Convert.ToInt32(item.Tag);
            UnderlineType unlinetp;
            switch (x)
            {
                case -1: // 无
                    unlinetp = UnderlineType.None;
                    break;
                case 0: // 单实线
                    unlinetp = UnderlineType.Single;
                    break;
                case 1: // 双实线
                    unlinetp = UnderlineType.Double;
                    break;
                case 2: // 虚线
                    unlinetp = UnderlineType.Dash;
                    break;
                default:
                    unlinetp = UnderlineType.None;
                    break;
            }
            editor.Document.Selection.CharacterFormat.Underline = unlinetp;
        }
       

        
        int i = 0;
        string old = "";
        /// <summary>
        /// RichEditBox输入改变唤醒附加属性
        /// </summary>
        /// <param name="sender">RichEditBox</param>
        /// <param name="e">事件参数</param>
        private void Editor_TextChanged(object sender, RoutedEventArgs e)
        {
            if (i == 0)
            {
                ++i;
                return;
            }
            
            RichEditBox edit = sender as RichEditBox;
            string str = "";
            edit.Document.GetText(TextGetOptions.FormatRtf, out str);
            if(str != old)
            {
                RtfText.SetRichText(edit, str);
                old = str;
            }
                 
        }
    }
}
