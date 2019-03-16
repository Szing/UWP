using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using NavDemo.Models;

namespace NavDemo.ViewModels
{

    [DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property propcmd for command
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性 propcmd 输入命令

        public MainPage_Model()
        {
            if (IsInDesignMode )
            {
                Title = "Title is a little different in Design mode";
            }
            IsPaneOpen = false;
            NavMenuItemList = new ObservableCollection<NavMenuItem>();
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf015", Label = "首页" });
            //NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf022", Label = "频道" });
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf002", Label = "搜索" });
            //NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf007", Label = "用户中心" });
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf05a", Label = "关于" });
        }

        //propvm tab tab string tab Title
        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<String> _TitleDefaultValueFactory = ()=>"Title is Here";
        #endregion




        /// <summary>
        /// 汉堡菜单列表
        /// </summary>
        public ObservableCollection<NavMenuItem> NavMenuItemList
        {
            get { return _NavMenuItemListLocator(this).Value; }
            set { _NavMenuItemListLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property ObservableCollection<HamburgerMenuItem> NavMenuItemList Setup        
        protected Property<ObservableCollection<NavMenuItem>> _NavMenuItemList = new Property<ObservableCollection<NavMenuItem>> { LocatorFunc = _NavMenuItemListLocator };
        static Func<BindableBase, ValueContainer<ObservableCollection<NavMenuItem>>> _NavMenuItemListLocator = RegisterContainerLocator<ObservableCollection<NavMenuItem>>("NavMenuItemList", model => model.Initialize("NavMenuItemList", ref model._NavMenuItemList, ref _NavMenuItemListLocator, _NavMenuItemListDefaultValueFactory));
        static Func<ObservableCollection<NavMenuItem>> _NavMenuItemListDefaultValueFactory = () => default(ObservableCollection<NavMenuItem>);
        #endregion

        /// <summary>
        /// 是否展开菜单
        /// </summary>
        public bool IsPaneOpen
        {
            get { return _IsPaneOpenLocator(this).Value; }
            set { _IsPaneOpenLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property bool IsPaneOpen Setup        
        protected Property<bool> _IsPaneOpen = new Property<bool> { LocatorFunc = _IsPaneOpenLocator };
        static Func<BindableBase, ValueContainer<bool>> _IsPaneOpenLocator = RegisterContainerLocator<bool>("IsPaneOpen", model => model.Initialize("IsPaneOpen", ref model._IsPaneOpen, ref _IsPaneOpenLocator, _IsPaneOpenDefaultValueFactory));
        static Func<bool> _IsPaneOpenDefaultValueFactory = () => default(bool);
        #endregion


        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        //{
        //    return base.OnBindedToView(view, oldValue);
        //}

        ///// <summary>
        ///// This will be invoked by view when this instance of viewmodel in ViewModel property is overwritten.
        ///// </summary>
        ///// <param name="view">Overwrite target view.</param>
        ///// <param name="newValue">The value replacing </param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnUnbindedFromView(MVVMSidekick.Views.IView view, IViewModel newValue)
        //{
        //    return base.OnUnbindedFromView(view, newValue);
        //}

        private bool isLoaded;
        /// <summary>
        /// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        /// </summary>
        /// <param name="view">View that firing Load event</param>
        /// <returns>Task awaiter</returns>
        protected override async Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            if (!isLoaded)
            {
                this.RegisterCommand();
                this.isLoaded = true;
            }
            await base.OnBindedViewLoad(view);
            //await StageManager["frameMain"].Show(new HomePage_Model());
        }

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}

        ///// <summary>
        ///// <para>If dispose actions got exceptions, will handled here. </para>
        ///// </summary>
        ///// <param name="exceptions">
        ///// <para>The exception and dispose infomation</para>
        ///// </param>
        //protected override async void OnDisposeExceptions(IList<DisposeInfo> exceptions)
        //{
        //    base.OnDisposeExceptions(exceptions);
        //    await TaskExHelper.Yield();
        //}

        #endregion


        private void RegisterCommand()
        {
            //一般列表项点击事件
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Object>()
                .Where(x => x.EventName == "NavToPage")
                     .Subscribe(
                         async e =>
                         {
                             NavMenuItem item = e.EventData as NavMenuItem;
                             if (item != null)
                             {
                                 switch (item.Label)
                                 {
                                     case "首页":
                                         this.IsPaneOpen = false;
                                         await StageManager["frameMain"].Show(new HomePage_Model());
                                         break;
                                     case "搜索":
                                         this.IsPaneOpen = false;
                                         await StageManager["frameMain"].Show(new SearchPage_Model());
                                         break;
                                     case "关于":
                                         this.IsPaneOpen = false;
                                         await StageManager["frameMain"].Show(new AboutPage_Model());
                                         break;
                                     default:
                                         break;
                                 }
                             }
                         }
                     ).DisposeWith(this);
        }
    }

}

