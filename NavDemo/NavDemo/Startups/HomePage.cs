using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using NavDemo;
using NavDemo.ViewModels;
using System;
using System.Net;
using System.Windows;


namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        static Action HomePageConfig =
           CreateAndAddToAllConfig(ConfigHomePage);

        public static void ConfigHomePage()
        {
            ViewModelLocator<HomePage_Model>
                .Instance
                .Register(context =>
                   GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<HomePage_Model>())
                .GetViewMapper()
                .MapToDefault<HomePage>();

        }
    }
}
