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
using NavDemo.Models;
using Windows.UI.Xaml.Controls;

namespace NavDemo.ViewModels
{

    [DataContract]
    public class HomePage_Model : ViewModelBase<HomePage_Model>
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


        public Dialog currentDialog { get => _currentDialogLocator(this).Value; set => _currentDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property Dialog currentDialog Setup        
        protected Property<Dialog> _currentDialog = new Property<Dialog> { LocatorFunc = _currentDialogLocator };
        static Func<BindableBase, ValueContainer<Dialog>> _currentDialogLocator = RegisterContainerLocator(nameof(currentDialog), m => m.Initialize(nameof(currentDialog), ref m._currentDialog, ref _currentDialogLocator, () => default(Dialog)));
        #endregion


        public Friend chosenFriend { get => _chosenFriendLocator(this).Value; set => _chosenFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend chosenFriend Setup        
        protected Property<Friend> _chosenFriend = new Property<Friend> { LocatorFunc = _chosenFriendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _chosenFriendLocator = RegisterContainerLocator(nameof(chosenFriend), m => m.Initialize(nameof(chosenFriend), ref m._chosenFriend, ref _chosenFriendLocator, () => default(Friend)));
        #endregion


        public List<Friend> friendItemList { get => _friendItemListLocator(this).Value; set => _friendItemListLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Friend> friendItemList Setup        
        protected Property<List<Friend>> _friendItemList = new Property<List<Friend>> { LocatorFunc = _friendItemListLocator };
        static Func<BindableBase, ValueContainer<List<Friend>>> _friendItemListLocator = RegisterContainerLocator(nameof(friendItemList), m => m.Initialize(nameof(friendItemList), ref m._friendItemList, ref _friendItemListLocator, () => default(List<Friend>)));
        #endregion


        public SuggestService suggest { get => _suggestLocator(this).Value; set => _suggestLocator(this).SetValueAndTryNotify(value); }
        #region Property SuggestService suggest Setup        
        protected Property<SuggestService> _suggest = new Property<SuggestService> { LocatorFunc = _suggestLocator };
        static Func<BindableBase, ValueContainer<SuggestService>> _suggestLocator = RegisterContainerLocator(nameof(suggest), m => m.Initialize(nameof(suggest), ref m._suggest, ref _suggestLocator, () => default(SuggestService)));
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


                              AutoSuggestBoxSuggestionChosenEventArgs args = (AutoSuggestBoxSuggestionChosenEventArgs)e.EventArgs.Parameter;
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


        public CommandModel<ReactiveCommand, String> CommandInsertDialog
        {
            get { return _CommandInsertDialogLocator(this).Value; }
            set { _CommandInsertDialogLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandInsertDialog Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandInsertDialog = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandInsertDialogLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandInsertDialogLocator = RegisterContainerLocator("CommandInsertDialog", m => m.Initialize("CommandInsertDialog", ref m._CommandInsertDialog, ref _CommandInsertDialogLocator,
              model =>
              {
                  var state = "CommandInsertDialog";
                  var commandId = "CommandInsertDialog";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add SomeCommand logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              
                              ServiceLocator.Instance.Resolve<DataService>()
                                 .InsertDialog(vm.currentDialog);
                             
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

        bool flag = false;
        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            //在第一次初始化时启动全局事件监听
            if (!flag)
            {
                TextChangedCommand();
                
                flag = true;
            }
            chosenFriend = new Friend();
            currentDialog = new Dialog();
            //建立Friend的数据表单
            DbContext.GetInstance().initTableFriend();
            //建立Dialog的数据表单
            DbContext.GetInstance().initTableDialog();
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
        private void TextChangedCommand()
        {
            //一般列表项点击事件
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Object>()
                .Where(x => x.EventName == "TextChangedEventRouter")
                     .Subscribe(
                          e =>
                          {
                              var i = (AutoSuggestBox)e.Sender;

                              friendItemList = ServiceLocator.Instance.Resolve<SuggestService>().Suggest(i.Text);
                          }
                     ).DisposeWith(this);
        }
    }

}

