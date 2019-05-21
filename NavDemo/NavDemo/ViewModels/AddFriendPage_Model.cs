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
    public class AddFriendPage_Model : ViewModelBase<AddFriendPage_Model>
    {
        // If you have install the code sniplets, use "propvm + [tab] +[tab]" create a property。
        // 如果您已经安装了 MVVMSidekick 代码片段，请用 propvm +tab +tab 输入属性

        public string Title { get => _TitleLocator(this).Value; set => _TitleLocator(this).SetValueAndTryNotify(value); }
        #region Property string Title Setup        
        protected Property<string> _Title = new Property<string> { LocatorFunc = _TitleLocator };
        static Func<BindableBase, ValueContainer<string>> _TitleLocator = RegisterContainerLocator(nameof(Title), m => m.Initialize(nameof(Title), ref m._Title, ref _TitleLocator, () => default(string)));
        #endregion

        /// <summary>
        /// 当前Friend
        /// </summary>
        public Friend friend { get => _friendLocator(this).Value; set => _friendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend friend Setup        
        protected Property<Friend> _friend = new Property<Friend> { LocatorFunc = _friendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _friendLocator = RegisterContainerLocator(nameof(friend), m => m.Initialize(nameof(friend), ref m._friend, ref _friendLocator, () => default(Friend)));
        #endregion


        /// <summary>
        /// 带参添加朋友，通过新建好友ID卡添加
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
                              if(vm.friend.nameFriend != null && vm.friend.nickNameFriend != null)
                              {
                                  ServiceLocator.Instance.Resolve<DataService>()
                                .InsertFriend(vm.friend);
                                  await new Windows.UI.Popups.MessageDialog("添加成功").ShowAsync();
                              }
                             
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              
                              //vm.suggest.insert(vm.friend);
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
            friend = new Friend();
            friend.iconFriend = "ms-appx:///Assets/light.jpg";
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

