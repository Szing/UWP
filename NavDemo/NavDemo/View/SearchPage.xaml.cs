
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
using NavDemo.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : MVVMPage
    {

        public SearchPage()
            : this(null)
        {

        }
        public SearchPage(SearchPage_Model model)
            : base(model)
        {
            this.InitializeComponent();
            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as SearchPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as SearchPage_Model;
        }


        public SearchPage_Model StrongTypeViewModel
        {
            get { return (SearchPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(SearchPage_Model), typeof(SearchPage), new PropertyMetadata(null));




        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void MyASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            MyASBox.ItemsSource = ServiceLocator.Instance.Resolve<SuggestService>().Suggest(MyASBox.Text);
            
        }

       
        /// <summary>
        /// 向上隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            if (relativePanel.Visibility == Visibility.Collapsed)
            {
                btnHide.Content = "︽点击看看";
                relativePanel.Visibility = Visibility.Visible;
               
            }
            else
            {
                btnHide.Content = "︾隐藏之后";
                
                relativePanel.Visibility = Visibility.Collapsed;
                
            }
            

        }
        /// <summary>
        /// 更换搜索模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(suggestButton.Visibility == Visibility.Collapsed)
            {
                suggestButton.Visibility = Visibility.Visible;
                MyASBox.Visibility = Visibility.Visible;
                dateButton.Visibility = Visibility.Collapsed;
                MemoryCalenderDatePicker.Visibility = Visibility.Collapsed;
            }
            else
            {
                dateButton.Visibility = Visibility.Visible;
                MemoryCalenderDatePicker.Visibility = Visibility.Visible;
                suggestButton.Visibility = Visibility.Collapsed;
                MyASBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
