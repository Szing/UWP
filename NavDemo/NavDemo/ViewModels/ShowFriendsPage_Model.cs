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

namespace NavDemo.ViewModels
{

    [DataContract]
    public class ShowFriendPage_Model : ViewModelBase<ShowFriendPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public string Title { get => _TitleLocator(this).Value; set => _TitleLocator(this).SetValueAndTryNotify(value); }
        #region Property string Title Setup        
        protected Property<string> _Title = new Property<string> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<string>> _TitleLocator = RegisterContainerLocator(nameof(Title), m => m.Initialize(nameof(Title), ref m._Title, ref _TitleLocator, () => default(string)));
        #endregion


        /// <summary>
        /// 当前展示的Friend
        /// </summary>
        public Friend currentFriend { get => _currentFriendLocator(this).Value; set => _currentFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend currentFriend Setup        
        protected Property<Friend> _currentFriend = new Property<Friend> { LocatorFunc = _currentFriendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _currentFriendLocator = RegisterContainerLocator(nameof(currentFriend), m => m.Initialize(nameof(currentFriend), ref m._currentFriend, ref _currentFriendLocator, () => default(Friend)));
        #endregion



        /// <summary>
        /// Friend列表
        /// </summary>
        public List<Friend> listFriend { get => _listFriendLocator(this).Value; set => _listFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Friend> listFriend Setup        
        protected Property<List<Friend>> _listFriend = new Property<List<Friend>> { LocatorFunc = _listFriendLocator };
        static Func<BindableBase, ValueContainer<List<Friend>>> _listFriendLocator = RegisterContainerLocator(nameof(listFriend), m => m.Initialize(nameof(listFriend), ref m._listFriend, ref _listFriendLocator, () => default(List<Friend>)));
        #endregion

        /// <summary>
        /// 当前Friend序号
        /// </summary>
        public int indexFriend { get => _indexFriendLocator(this).Value; set => _indexFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property int indexFriend Setup        
        protected Property<int> _indexFriend = new Property<int> { LocatorFunc = _indexFriendLocator };
        static Func<BindableBase, ValueContainer<int>> _indexFriendLocator = RegisterContainerLocator(nameof(indexFriend), m => m.Initialize(nameof(indexFriend), ref m._indexFriend, ref _indexFriendLocator, () => default(int)));
        #endregion

        /// <summary>
        /// 转到上一个Friend
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandToLastFriend
        {
            get { return _CommandToLastFriendLocator(this).Value; }
            set { _CommandToLastFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandToLastFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandToLastFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandToLastFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandToLastFriendLocator = RegisterContainerLocator("CommandToLastFriend", m => m.Initialize("CommandToLastFriend", ref m._CommandToLastFriend, ref _CommandToLastFriendLocator,
              model =>
              {
                  var state = "CommandToLastFriend";
                  var commandId = "CommandToLastFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add ToLastFriend logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();

                              if (vm.listFriend != null && vm.indexFriend > 0)
                              {
                                  vm.indexFriend -= 1;
                                  vm.currentFriend = vm.listFriend[vm.indexFriend];
                              }
                              else
                              {
                                  await new Windows.UI.Popups.MessageDialog("It's the Friend 0").ShowAsync();
                              }
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
        /// 转到下一个Friend
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandToNextFriend
        {
            get { return _CommandToNextFriendLocator(this).Value; }
            set { _CommandToNextFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandToNextFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandToNextFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandToNextFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandToNextFriendLocator = RegisterContainerLocator("CommandToNextFriend", m => m.Initialize("CommandToNextFriend", ref m._CommandToNextFriend, ref _CommandToNextFriendLocator,
              model =>
              {
                  var state = "CommandToNextFriend";
                  var commandId = "CommandToNextFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add ToNextFriend logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              if (vm.listFriend != null && vm.indexFriend + 1 < vm.listFriend.Count())
                              {
                                  vm.indexFriend += 1;
                                  vm.currentFriend = vm.listFriend[vm.indexFriend];

                              }
                              else
                              {
                                  await new Windows.UI.Popups.MessageDialog("It's the friend MAX").ShowAsync();
                              }
                              
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
        /// 删除当前Friend
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandDeleteFriend
        {
            get { return _CommandDeleteFriendLocator(this).Value; }
            set { _CommandDeleteFriendLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDeleteFriend Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandDeleteFriend = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDeleteFriendLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDeleteFriendLocator = RegisterContainerLocator("CommandDeleteFriend", m => m.Initialize("CommandDeleteFriend", ref m._CommandDeleteFriend, ref _CommandDeleteFriendLocator,
              model =>
              {
                  var state = "CommandDeleteFriend";
                  var commandId = "CommandDeleteFriend";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add DeleteFriend logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              //获取数据库服务
                              DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
                              dataService.DeleteFriend(vm.currentFriend.idFriend);
                              vm.listFriend.Remove(vm.currentFriend);
                              if (vm.listFriend.Count == 0)
                              {
                                  vm.currentFriend.iconFriend = "ms-appx:///Assets/light.jpg";
                                  vm.currentFriend.nameFriend = "null";
                                  vm.currentFriend.nickNameFriend = "null";
                              }
                              else
                              {
                                  if (vm.indexFriend == 0)
                                  {
                                      vm.currentFriend = vm.listFriend[0];
                                  }
                                  else
                                  {
                                      --vm.indexFriend;
                                      vm.currentFriend = vm.listFriend[vm.indexFriend];
                                  }

                              }

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
        /// 加入新Friend
        /// </summary>
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
                              await
                               CastToCurrentType(model)
                               .StageManager
                               .DefaultStage
                               .Show(ServiceLocator.Instance.Resolve<AddFriendPage_Model>());
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

        #region OnBindedToView
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
        #endregion

        #region OnUnbindedFromView
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
        #endregion

        #region OnBindedViewLoad

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            //获取数据库服务
            DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
            //获取好友列表
            listFriend = dataService.GetAllFriends();

            currentFriend = new Friend();
            if(listFriend.Count != 0)
            {
                currentFriend = listFriend[0];
            }
            else
            {
                currentFriend.iconFriend = "ms-appx:///Assets/light.jpg";
                currentFriend.idFriend = -1;
                currentFriend.nameFriend = "null";
                currentFriend.nickNameFriend = "null";

            }
            
            return base.OnBindedViewLoad(view);
        }
        #endregion

        #region OnBindedViewUnload

        ///// <summary>
        ///// This will be invoked by view when the view fires Unload event and this viewmodel instance is still in view's  ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Unload event</param>
        ///// <returns>Task awaiter</returns>
        //protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        //{
        //    return base.OnBindedViewUnload(view);
        //}
        #endregion

        #region OnDisposeExceptions

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

        #endregion

    }
}

