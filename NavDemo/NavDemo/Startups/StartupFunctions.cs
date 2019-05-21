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

        /// <summary>
        /// 用于注册各类的ViewModelLocator
        /// </summary>
		public static void RunAllConfig()
		{
			if (AllConfig==null) return;


            SimpleIoc.Default.Register<HomePage_Model>();
            SimpleIoc.Default.Register<AboutPage_Model>();
            SimpleIoc.Default.Register<SearchPage_Model>();
            SimpleIoc.Default.Register<AddFriendPage_Model>();
            SimpleIoc.Default.Register<ShowFriendPage_Model>();

            SimpleIoc.Default.Register<IDbContentService, DbContext>();
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<ISuggestService, SuggestService>();
            SimpleIoc.Default.Register<IFileService, FileService>();
            SimpleIoc.Default.Register<IImageService, ImageService>();

            Services.ServiceLocator.Instance.Register<DbContext>(DbContext.GetInstance());
            Services.ServiceLocator.Instance.Register<DataService>(DataService.GetInstance());
            Services.ServiceLocator.Instance.Register<SuggestService>(SuggestService.GetInstance());
            Services.ServiceLocator.Instance.Register<FileService>(FileService.GetInstance());
            Services.ServiceLocator.Instance.Register<ImageService>(ImageService.GetInstance());

            foreach (var item in AllConfig)
			{
				item();
			}

            

            
            
            
            
        }


    }
}
