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
                             DbContext.GetInstance().Init();
                             
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

        private void MyASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //MyASBox.ItemsSource = ServiceLocator.Instance.Resolve<SuggestService>().Suggest(MyASBox.Text);]
            friendItemList =  ServiceLocator.Instance.Resolve<SuggestService>().Suggest(sender.Text);
        }
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

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            DbContext.GetInstance().Init();
            StringBuilder sb = new StringBuilder();
            DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
            friendItemList = dataService.GetAllFriends();

            foreach (Friend item in friendItemList)
            {
                sb.AppendLine($"{item.idFriend} {item.nameFriend} {item.nickNameFriend} ");
            }
            ServiceLocator.Instance.Resolve<SuggestService>().init(friendItemList);
            new Windows.UI.Popups.MessageDialog(sb.ToString()).ShowAsync();

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




    }

}

