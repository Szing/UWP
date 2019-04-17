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
        static Action SearchPageConfig =
           CreateAndAddToAllConfig(ConfigSearchPage);

        public static void ConfigSearchPage()
        {
            ViewModelLocator<SearchPage_Model>
                .Instance
                .Register(context =>
                    GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<SearchPage_Model>())
                .GetViewMapper()
                .MapToDefault<SearchPage>();

        }
    }
}
