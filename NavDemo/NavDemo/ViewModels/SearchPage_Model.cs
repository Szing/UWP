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
using NavDemo.Services;
using Windows.UI.Xaml.Controls;

namespace NavDemo.ViewModels
{

    [DataContract]
    public class SearchPage_Model : ViewModelBase<SearchPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性
        public SearchPage_Model()
        {
            if (IsInDesignMode)
            {
                Title = "Title is a little different in Design mode";
            }
            
        }
        public String Title
        {
            get { return _TitleLocator(this).Value; }
            set { _TitleLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String Title Setup
        protected Property<String> _Title = new Property<String> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<String>> _TitleLocator = RegisterContainerLocator<String>("Title", model => model.Initialize("Title", ref model._Title, ref _TitleLocator, _TitleDefaultValueFactory));
        static Func<BindableBase, String> _TitleDefaultValueFactory = m => m.GetType().Name;
        #endregion


        public List<Dialog> listDialog { get => _listDialogLocator(this).Value; set => _listDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Dialog> listDialog Setup        
        protected Property<List<Dialog>> _listDialog = new Property<List<Dialog>> { LocatorFunc = _listDialogLocator };
        static Func<BindableBase, ValueContainer<List<Dialog>>> _listDialogLocator = RegisterContainerLocator(nameof(listDialog), m => m.Initialize(nameof(listDialog), ref m._listDialog, ref _listDialogLocator, () => default(List<Dialog>)));
        #endregion


        public SuggestService suggest { get => _suggestLocator(this).Value; set => _suggestLocator(this).SetValueAndTryNotify(value); }
        #region Property SuggestService suggest Setup        
        protected Property<SuggestService> _suggest = new Property<SuggestService> { LocatorFunc = _suggestLocator };
        static Func<BindableBase, ValueContainer<SuggestService>> _suggestLocator = RegisterContainerLocator(nameof(suggest), m => m.Initialize(nameof(suggest), ref m._suggest, ref _suggestLocator, () => default(SuggestService)));
        #endregion



        public Friend friend { get => _friendLocator(this).Value; set => _friendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend friend Setup        
        protected Property<Friend> _friend = new Property<Friend> { LocatorFunc = _friendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _friendLocator = RegisterContainerLocator(nameof(friend), m => m.Initialize(nameof(friend), ref m._friend, ref _friendLocator, () => default(Friend)));
        #endregion


        public Friend chosenFriend { get => _chosenFriendLocator(this).Value; set => _chosenFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend chosenFriend Setup        
        protected Property<Friend> _chosenFriend = new Property<Friend> { LocatorFunc = _chosenFriendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _chosenFriendLocator = RegisterContainerLocator(nameof(chosenFriend), m => m.Initialize(nameof(chosenFriend), ref m._chosenFriend, ref _chosenFriendLocator, () => default(Friend)));
        #endregion

        /// <summary>
        /// 用于AutoSuggestion的suggest项目
        /// </summary>
        public  List<Friend> friendItemList { get => _friendItemListLocator(this).Value; set => _friendItemListLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Friend> friendItemList Setup        
        protected Property<List<Friend>> _friendItemList = new Property<List<Friend>> { LocatorFunc = _friendItemListLocator };
        static Func<BindableBase, ValueContainer<List<Friend>>> _friendItemListLocator = RegisterContainerLocator(nameof(friendItemList), m => m.Initialize(nameof(friendItemList), ref m._friendItemList, ref _friendItemListLocator, () => default(List<Friend>)));
        #endregion


        public CommandModel<ReactiveCommand, String> CommandAddFriend
        {
            get { return _CommandAddFriendLocator(this).Value; }
            set { _CommandAddFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandAddFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandAddFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandAddFriendLocator };

        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandAddFriendLocator = RegisterContainerLocator("CommandAddFriend", m => m.Initialize("CommandAddFriend", ref m._CommandAddFriend, ref _CommandAddFriendLocator,
              model =>
              {
                  var state = "CommandAddFriend";
                  var commandId = "CommandAddFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add AddFriend logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              ServiceLocator.Instance.Resolve<DataService>()
                                 .InsertFriend();
                              
                          })
                      .DoNotifyDefaultEventRouter(vm, commandId)
                      .Subscribe()
                      .DisposeWith(vm);

                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion



        public CommandModel<ReactiveCommand, String> CommandGetFriends
        {
            get { return _CommandGetFriendsLocator(this).Value; }
            set { _CommandGetFriendsLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGetFriends Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGetFriends = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetFriendsLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetFriendsLocator = RegisterContainerLocator("CommandGetFriends", m => m.Initialize("CommandGetFriends", ref m._CommandGetFriends, ref _CommandGetFriendsLocator,
              model =>
              {

                  var state = "CommandGetFriends";
                  var commandId = "CommandGetFriends";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add GetFriends logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              StringBuilder sb = new StringBuilder();
                              DataService dataService = ServiceLocator.Instance.Resolve<DataService>();

                              List<Friend> list = dataService.GetAllFriends();
                              foreach (Friend item in list)
                              {
                                  sb.AppendLine($"{item.idFriend} {item.nameFriend} {item.nickNameFriend} ");
                              }
                              await new Windows.UI.Popups.MessageDialog(sb.ToString()).ShowAsync();
                          })
                      .DoNotifyDefaultEventRouter(vm, commandId)
                      .Subscribe()
                      .DisposeWith(vm);

                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion



        public CommandModel<ReactiveCommand, String> CommandTableInit
        {
            get { return _CommandTableInitLocator(this).Value; }
            set { _CommandTableInitLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandTableInit Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandTableInit = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandTableInitLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandTableInitLocator = RegisterContainerLocator("CommandTableInit", m => m.Initialize("CommandTableInit", ref m._CommandTableInit, ref _CommandTableInitLocator,
              model =>
              {
                  var state = "CommandTableInit";
                  var commandId = "CommandTableInit";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                         vm,
                         async e =>
                         {
                             //Todo: Add SomeCommand logic here, or
                             await MVVMSidekick.Utilities.TaskExHelper.Yield();
                             DbContext.GetInstance().initTableFriend();
                             
                         })
                     .DoNotifyDefaultEventRouter(vm, commandId)
                     .Subscribe()
                     .DisposeWith(vm);
                  

                  
                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion


        public CommandModel<ReactiveCommand, String> CommandSubmitFriend
        {
            get { return _CommandSubmitFriendLocator(this).Value; }
            set { _CommandSubmitFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandSubmitFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandSubmitFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandSubmitFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandSubmitFriendLocator = RegisterContainerLocator("CommandSubmitFriend", m => m.Initialize("CommandSubmitFriend", ref m._CommandSubmitFriend, ref _CommandSubmitFriendLocator,
              model =>
              {
                  var state = "CommandSubmitFriend";
                  var commandId = "CommandSubmitFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add FriendSubmit logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              StringBuilder sb = new StringBuilder();
                              DataService dataService = ServiceLocator.Instance.Resolve<DataService>();

                              List<Dialog> list = dataService.GetDialogs(vm.chosenFriend.idFriend);
                              vm.listDialog = list;
                              

                          })
                      .DoNotifyDefaultEventRouter(vm, commandId)
                      .Subscribe()
                      .DisposeWith(vm);

                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion


        public CommandModel<ReactiveCommand, String> CommandInsertFriend
        {
            get { return _CommandInsertFriendLocator(this).Value; }
            set { _CommandInsertFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandInsertFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandInsertFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandInsertFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandInsertFriendLocator = RegisterContainerLocator("CommandInsertFriend", m => m.Initialize("CommandInsertFriend", ref m._CommandInsertFriend, ref _CommandInsertFriendLocator,
              model =>
              {
                  var state = "CommandInsertFriend";
                  var commandId = "CommandInsertFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add InsertFriend logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              ServiceLocator.Instance.Resolve<DataService>()
                                 .InsertFriend(vm.friend);
                              vm.suggest.insert(vm.friend);
                          })
                      .DoNotifyDefaultEventRouter(vm, commandId)
                      .Subscribe()
                      .DisposeWith(vm);

                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion

        /// <summary>
        /// 将被选中的项放到chosenFriend中
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandChoseFriend
        {
            get { return _CommandCommandChoseFriendLocator(this).Value; }
            set { _CommandCommandChoseFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandCommandChoseFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandCommandChoseFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandCommandChoseFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandCommandChoseFriendLocator = RegisterContainerLocator("CommandCommandChoseFriend", m => m.Initialize("CommandCommandChoseFriend", ref m._CommandCommandChoseFriend, ref _CommandCommandChoseFriendLocator,
              model =>
              {
                  var state = "CommandCommandChoseFriend";
                  var commandId = "CommandCommandChoseFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };
                 
                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add CommandChoseFriend logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();


                              AutoSuggestBoxSuggestionChosenEventArgs args= (AutoSuggestBoxSuggestionChosenEventArgs) e.EventArgs.Parameter;
                              vm.chosenFriend = (Friend)args.SelectedItem;
                             
                              
                             //await new Windows.UI.Popups.MessageDialog(vm.chosenFriend.nameFriend).ShowAsync();
                          })
                      .DoNotifyDefaultEventRouter(vm, commandId)
                      .Subscribe()
                      .DisposeWith(vm);

                  var cmdmdl = cmd.CreateCommandModel(state);

                  cmdmdl.ListenToIsUIBusy(
                      model: vm,
                      canExecuteWhenBusy: false);
                  return cmdmdl;
              }));
        #endregion

        #region Life Time Event Handling

        ///// <summary>
        ///// This will be invoked by view when this viewmodel instance is set to view's ViewModel property. 
        ///// </summary>
        ///// <param name="view">Set target</param>
        ///// <param name="oldValue">Value before set.</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedToView(MVVMSidekick.Views.IView view, IViewModel oldValue)
        ///{

        /// return base.OnBindedToView(view, oldValue);
        ///}

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
        bool flag = false;
        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            //在第一次初始化时启动全局事件监听
            if(!flag)
            {
                TChangedCommand();
                flag = true;
            }
            friend = new Friend() { nameFriend = "unknow" ,nickNameFriend = "unknow" };
            chosenFriend = new Friend();

            //建立Friend的数据表单
            DbContext.GetInstance().initTableFriend();
            //获取数据库服务
            DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
            //获取suggest服务
            suggest = ServiceLocator.Instance.Resolve<SuggestService>();
            //初始化suggst项目总体
            suggest.init(dataService.GetAllFriends());
            //初始化suggest下拉表单
            friendItemList = dataService.GetAllFriends();

            

            return base.OnBindedViewLoad(view);
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
        
        

        //TChangedEventRouter
        private void TChangedCommand()
        {
            //一般列表项点击事件
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Object>()
                .Where(x => x.EventName == "TChangedEventRouter")
                     .Subscribe(
                          e =>
                         {
                             var i = (AutoSuggestBox)e.Sender;
                           
                             friendItemList =  ServiceLocator.Instance.Resolve<SuggestService>().Suggest(i.Text); 
                         }
                     ).DisposeWith(this);
        }
       
    }

}

