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
using MVVMSidekick.Services;
using NavDemo.Services;
using System.Threading.Tasks;
using NavDemo.Models;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NavDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : MVVMPage
    {
        private double x = 0;
        private double y = 0;
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
            initializeFrostedGlass(GlassHost);
            initializeFrostedGlass(GlassHost1);
            ApplicationView view = ApplicationView.GetForCurrentView();
            //将标题栏的三个键背景设为透明
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            //失去焦点时，将三个键背景设为透明
            view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            //失去焦点时，将三个键前景色设为白色
            view.TitleBar.ButtonInactiveForegroundColor = Colors.White;
            
            //X方向滑动打开汉堡菜单服务
            this.ManipulationMode = ManipulationModes.TranslateX;
            this.ManipulationCompleted += The_ManipulationCompleted;
            this.ManipulationDelta += The_ManipulationDelta;

            Page_Loaded();
        }

        /// <summary>
        /// 修改TitleBar的样式
        /// </summary>
        void ApplyColorToTitleBar()
        {
            var view = ApplicationView.GetForCurrentView();

            // active
            view.TitleBar.BackgroundColor = Colors.DarkBlue;
            view.TitleBar.ForegroundColor = Colors.White;

            // inactive
            view.TitleBar.InactiveBackgroundColor = Colors.LightGray;
            view.TitleBar.InactiveForegroundColor = Colors.Gray;

            // button
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonForegroundColor = Colors.White;

            view.TitleBar.ButtonHoverBackgroundColor = Colors.LightSkyBlue;
            view.TitleBar.ButtonHoverForegroundColor = Colors.White;

            view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(255, 0, 0, 120);
            view.TitleBar.ButtonPressedForegroundColor = Colors.White;

            view.TitleBar.ButtonInactiveBackgroundColor = Colors.DarkGray;
            view.TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
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

        private void The_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            x += e.Delta.Translation.X;     //将滑动的值赋给x 
            y += e.Delta.Translation.Y;     //将滑动的值赋给y

        }

        private void The_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (x > 100)
                RootSplitView.IsPaneOpen = true;
            if (x < -100)
                RootSplitView.IsPaneOpen = false;
            x = 0;

            if (y > 100)
            {
                if (y < 500)
                {
                    StrongTypeViewModel.CommandNextPage.Execute(1);
                }
                else
                {
                    StrongTypeViewModel.CommandNextPage.Execute(3);
                }
            }
            else if (y < -100)
            {
                if (y > -500)
                {
                    StrongTypeViewModel.CommandNextPage.Execute(2);
                }
                else
                {
                    StrongTypeViewModel.CommandNextPage.Execute(4);
                }
            }
            y = 0;
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

        private async void  Button_Click(object sender, RoutedEventArgs e)
        {
            //ServiceLocator.Instance.Resolve<FileService>().WriteDialog("dialog.txt");
            await ServiceLocator.Instance.Resolve<FileService>().ReadDialog("dialog.txt");
        }

        private List<FireflyParticle> FireflyParticle { set; get; } = new List<FireflyParticle>();

        private void Page_Loaded()
        {
            if (!FireflyParticle.Any())
            {
                Rect bound = new Rect(0, 0,1920,1280);
                for (int i = 0; i < 100; i++)
                {
                    FireflyParticle.Add(new FireflyParticle(bound));
                }
            }
        }

        private void Canvas_OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            foreach (var temp in FireflyParticle)
            {
                temp.Time(args.Timing.ElapsedTime);
            }
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            using (var session = args.DrawingSession)
            {
                foreach (var temp in FireflyParticle)
                {
                    using (var cl = new CanvasCommandList(session))
                    using (var ds = cl.CreateDrawingSession())
                    {
                        var c = temp.CenterColor;
                        c.A = (byte)(temp.OpColor * 255);
                        ds.FillCircle((float)temp.Point.X, (float)temp.Point.Y, (float)temp.Radius, c);
                        using (var glow = new GlowEffectGraph())
                        {
                            glow.Setup(cl, temp.Radius);
                            session.DrawImage(glow.Blur);
                        }
                    }
                }
            }
        }

    }
}
