using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection ;
using NavDemo.Services;
using NavDemo.ViewModels;
using GalaSoft.MvvmLight.Ioc;

namespace MVVMSidekick.Startups
{
    internal static partial class StartupFunctions
    {
        
       
      	static List<Action> AllConfig ;

		public static Action CreateAndAddToAllConfig(this Action action)
		{
			if (AllConfig == null)
			{
				AllConfig = new List<Action>();
			}
			AllConfig.Add(action);
			return action;
		}
		public static void RunAllConfig()
		{
			if (AllConfig==null) return;
			foreach (var item in AllConfig)
			{
				item();
			}
            SimpleIoc.Default.Register<HomePage_Model>();
            SimpleIoc.Default.Register<SearchPage_Model>();
            SimpleIoc.Default.Register<ShowFriendPage_Model>();
            SimpleIoc.Default.Register<AddFriendPage_Model>();

            Services.ServiceLocator.Instance.Register<DbContext>(DbContext.GetInstance());
            Services.ServiceLocator.Instance.Register<DataService>(DataService.GetInstance());
            Services.ServiceLocator.Instance.Register<SuggestService>(SuggestService.GetInstance());
            Services.ServiceLocator.Instance.Register<FileService>(FileService.GetInstance());
            Services.ServiceLocator.Instance.Register<ImageService>(ImageService.GetInstance());
            
            Services.ServiceLocator.Instance.Register<HomePage_Model>(SimpleIoc.Default.GetInstance<HomePage_Model>());
            Services.ServiceLocator.Instance.Register<SearchPage_Model>(SimpleIoc.Default.GetInstance<SearchPage_Model>());
            Services.ServiceLocator.Instance.Register<ShowFriendPage_Model>(SimpleIoc.Default.GetInstance<ShowFriendPage_Model>());
            Services.ServiceLocator.Instance.Register<AddFriendPage_Model>(SimpleIoc.Default.GetInstance<AddFriendPage_Model>());
        }


    }
}
