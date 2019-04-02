using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection ;
using NavDemo.Services;

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
            Services.ServiceLocator.Instance.Register<DataService>(DataService.GetInstance());
            Services.ServiceLocator.Instance.Register<SuggestService>(SuggestService.GetInstance());
            Services.ServiceLocator.Instance.Register<FileService>(FileService.GetInstance());
            Services.ServiceLocator.Instance.Register<ImageService>(ImageService.GetInstance());
        }


    }
}
