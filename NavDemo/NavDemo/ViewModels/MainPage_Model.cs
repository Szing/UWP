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
using MVVMSidekick.EventRouting;
using NavDemo.Services;
using Windows.UI.Xaml.Media;

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
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf044", Label = "首页" });
            //NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf022", Label = "频道" });
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf002", Label = "搜索" });
            //NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf007", Label = "用户中心" });
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf007", Label = "好友中心" });
            NavMenuItemList.Add(new NavMenuItem { Glyph = "\uf067", Label = "添加好友" });

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


        



        public string listViewBackGround { get => _listViewBackGroundLocator(this).Value; set => _listViewBackGroundLocator(this).SetValueAndTryNotify(value); }
        #region Property string listViewBackGround Setup        
        protected Property<string> _listViewBackGround = new Property<string> { LocatorFunc = _listViewBackGroundLocator};
        static Func<BindableBase, ValueContainer<string>> _listViewBackGroundLocator = RegisterContainerLocator(nameof(listViewBackGround), m => m.Initialize(nameof(listViewBackGround), ref m._listViewBackGround, ref _listViewBackGroundLocator, () => default(string)));
        #endregion


        #region Life Time Event Handlingp

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

        //检测初始化的标记
        private bool isLoaded = false;
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

            //初始化ListView背景
            listViewBackGround = "/Assets/background1.png";
            //建立Friend的数据表单
            ServiceLocator.Instance.Resolve<DbContext>().initTableFriend();
            //建立Dialog的数据表单
            ServiceLocator.Instance.Resolve<DbContext>().initTableDialog();
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

        /// <summary>
        /// 汉堡菜单点击事件监听器
        /// </summary>
        private void RegisterCommand()
        {
            //一般列表项点击事件
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Object>()
                .Where(x => x.EventName == "NavToPage")
                     .Subscribe(
                         async e =>
                         {
                             var i = e;
                             NavMenuItem item = e.EventData as NavMenuItem;
                             if (item != null)
                             {
                                 switch (item.Label)
                                 {
                                     case "首页":
                                         this.IsPaneOpen = false;
                                         listViewBackGround = "/Assets/background1.png";
                                         await StageManager["frameMain"].Show(ViewModelLocator< HomePage_Model>.Instance.Resolve());
                                         break;
                                     case "搜索":
                                         this.IsPaneOpen = false;
                                         listViewBackGround = "/Assets/dushuren.png";
                                         await StageManager["frameMain"].Show(ViewModelLocator<SearchPage_Model>.Instance.Resolve());
                                         break;
                                     case "好友中心":
                                         this.IsPaneOpen = false;
                                         listViewBackGround = "/Assets/mozi.png";
                                         await StageManager["frameMain"].Show(ViewModelLocator<ShowFriendPage_Model>.Instance.Resolve());
                                         break;
                                     case "添加好友":
                                         this.IsPaneOpen = false;
                                         await StageManager["frameMain"].Show(ViewModelLocator<AddFriendPage_Model>.Instance.Resolve());
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

