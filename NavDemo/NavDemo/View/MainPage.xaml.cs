﻿using MVVMSidekick.Views;
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
using NavDemo.ViewModels;
using Windows.Phone.UI.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Composition;
using Windows.UI.ViewManagement;
using Windows.UI;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : MVVMPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            //回退功能
            SystemNavigationManager navmgr = SystemNavigationManager.GetForCurrentView();
            navmgr.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            navmgr.BackRequested += navmgr_BackRequested;

            this.RegisterPropertyChangedCallback(ViewModelProperty, (_, __) =>
            {
                StrongTypeViewModel = this.ViewModel as MainPage_Model;
            });
            StrongTypeViewModel = this.ViewModel as MainPage_Model;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            ApplicationView view = ApplicationView.GetForCurrentView();

            //将标题栏的三个键背景设为透明
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            //失去焦点时，将三个键背景设为透明
            view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            //失去焦点时，将三个键前景色设为白色
            view.TitleBar.ButtonInactiveForegroundColor = Colors.White; 
            

        }
        /// <summary>
        /// 回退事件
        /// </summary>
        /// <param name="sender">回退事件触发者</param>
        /// <param name="e">回退事件参数</param>
        private void navmgr_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame root = mainFrame;
            if (root != null)
            {
                if (root.CanGoBack)
                {
                    e.Handled = true;
                    root.GoBack();
                }
                
            }
        }
        /// <summary>
        /// 毛玻璃效果
        /// </summary>
        /// <param name="glassHost">需要毛玻璃化的组件</param>
        private void initializeFrostedGlass(UIElement glassHost)
        {

            Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
            Compositor compositor = hostVisual.Compositor;
            var backdropBrush = compositor.CreateHostBackdropBrush();
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = backdropBrush;
            ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
            base.OnNavigatedFrom(e);
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            //throw new NotImplementedException();
            var vm = this.LayoutRoot.DataContext as MainPage_Model;
            if (vm != null)
            {
                if (vm.IsPaneOpen)
                {
                    e.Handled = true;
                    vm.IsPaneOpen = false;
                }
            }
        }



        public MainPage_Model StrongTypeViewModel
        {
            get { return (MainPage_Model)GetValue(StrongTypeViewModelProperty); }
            set { SetValue(StrongTypeViewModelProperty, value); }
        }

        public static readonly DependencyProperty StrongTypeViewModelProperty =
                    DependencyProperty.Register("StrongTypeViewModel", typeof(MainPage_Model), typeof(MainPage), new PropertyMetadata(null));

        
    }
}
