
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ServiceLocator.Instance.Resolve<FileService>().SetRandomAccessStream(editor);

        }

        private void FontButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            var font = item.FontFamily;
            editor.FontFamily = font;
        }

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

        private void FontSizeButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            // 获取字号
            float size = Convert.ToSingle(item.Tag);

            editor.Document.Selection.CharacterFormat.Size = size;
        }

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

        private async void   ImageButton_Click(object sender, RoutedEventArgs e)
        {
           
            var stream = await ServiceLocator.Instance.Resolve<ImageService>().GetImageStream();
            editor.Document.Selection.InsertImage(200, 100, 0, Windows.UI.Text.VerticalCharacterAlignment.Baseline, "图像", stream);
            

        }

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

        private void Editor_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            MenuFlyout menu = FlyoutBase.GetAttachedFlyout(editor) as MenuFlyout;
            menu?.ShowAt(editor, new Point(e.CursorLeft, e.CursorTop));
        }
        private void OnCopy(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.Copy();
        }

        private void OnCut(object sender, RoutedEventArgs e)
        {
            editor.Document.Selection.Cut();
        }

        private void OnPaste(object sender, RoutedEventArgs e)
        {
            // Paste方法带有一个整型参数，表示要粘贴的格式
            editor.Document.Selection.Paste(0);
        }
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
    }
}
