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
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using NavDemo.Services;
using System.Diagnostics;

namespace NavDemo.ViewModels
{

    [DataContract]
    public class AboutPage_Model : ViewModelBase<AboutPage_Model>
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

        /// <summary>
        /// 当前的日志
        /// </summary>
        public Dialog currentDialog { get => _currentDialogLocator(this).Value; set => _currentDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property Dialog currentDialog Setup        
        protected Property<Dialog> _currentDialog = new Property<Dialog> { LocatorFunc = _currentDialogLocator };
        static Func<BindableBase, ValueContainer<Dialog>> _currentDialogLocator = RegisterContainerLocator(nameof(currentDialog), m => m.Initialize(nameof(currentDialog), ref m._currentDialog, ref _currentDialogLocator, () => default(Dialog)));
        #endregion

        /// <summary>
        /// 日志列表
        /// </summary>
        public List<Dialog> listDialog { get => _listDialogLocator(this).Value; set => _listDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Dialog> listDialog Setup        
        protected Property<List<Dialog>> _listDialog = new Property<List<Dialog>> { LocatorFunc = _listDialogLocator };
        static Func<BindableBase, ValueContainer<List<Dialog>>> _listDialogLocator = RegisterContainerLocator(nameof(listDialog), m => m.Initialize(nameof(listDialog), ref m._listDialog, ref _listDialogLocator, () => default(List<Dialog>)));
        #endregion

        /// <summary>
        /// 日志索引
        /// </summary>
        public int indexDialog { get => _indexDialogLocator(this).Value; set => _indexDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property int indexDialog Setup        
        protected Property<int> _indexDialog = new Property<int> { LocatorFunc = _indexDialogLocator };
        static Func<BindableBase, ValueContainer<int>> _indexDialogLocator = RegisterContainerLocator(nameof(indexDialog), m => m.Initialize(nameof(indexDialog), ref m._indexDialog, ref _indexDialogLocator, () => default(int)));
        #endregion

        /// <summary>
        /// 富文本框实例，在这里使用是真的非常不好的一件事
        /// </summary>
        public RichEditBox editBox { get => _editBoxLocator(this).Value; set => _editBoxLocator(this).SetValueAndTryNotify(value); }
        #region Property RichEditBox editBox Setup        
        protected Property<RichEditBox> _editBox = new Property<RichEditBox> { LocatorFunc = _editBoxLocator };
        static Func<BindableBase, ValueContainer<RichEditBox>> _editBoxLocator = RegisterContainerLocator(nameof(editBox), m => m.Initialize(nameof(editBox), ref m._editBox, ref _editBoxLocator, () => default(RichEditBox)));
        #endregion

       
        /// <summary>
        /// 切换到上一个日志
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandToLastPage
        {
            get { return _CommandToLastPageLocator(this).Value; }
            set { _CommandToLastPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandToLastPage Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandToLastPage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandToLastPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandToLastPageLocator = RegisterContainerLocator("CommandToLastPage", m => m.Initialize("CommandToLastPage", ref m._CommandToLastPage, ref _CommandToLastPageLocator,
              model =>
              {
                  var state = "CommandToLastPage";
                  var commandId = "CommandToLastPage";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ToLastPage logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              if(vm.listDialog != null && vm.indexDialog+1 < vm.listDialog.Count())
                              {
                                  vm.indexDialog += 1;
                                  vm.currentDialog = vm.listDialog[vm.indexDialog];
                                  if (vm.currentDialog.textDialog == null)
                                      vm.currentDialog.textDialog = "default.rtf";
                                  RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;

                                  Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                            await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream(vm.currentDialog.textDialog);

                                  if (randAccStream != null)
                                  {
                                      // Load the file into the Document property of the RichEditBox.
                                      richEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                                  }
                              }
                              else
                              {
                                  await new Windows.UI.Popups.MessageDialog("It's the page 0").ShowAsync();
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
        /// 切换到下一个日志
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandToNextPage
        {
            get { return _CommandToNextPageLocator(this).Value; }
            set { _CommandToNextPageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandToNextPage Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandToNextPage = new Property<CommandModel<ReactiveCommand, String>> {LocatorFunc = _CommandToNextPageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandToNextPageLocator = RegisterContainerLocator("CommandToNextPage", m => m.Initialize("CommandToNextPage", ref m._CommandToNextPage, ref _CommandToNextPageLocator,
              model =>
              {
                  var state = "CommandToNextPage";
                  var commandId = "CommandToNextPage";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ToNextPage logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              if (vm.listDialog != null &&vm.indexDialog > 0)
                              {
                                  vm.indexDialog -= 1;
                                  vm.currentDialog = vm.listDialog[vm.indexDialog];
                                  if (vm.currentDialog.textDialog == null)
                                      vm.currentDialog.textDialog = "default.rtf";
                                  RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;

                                  Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                            await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream(vm.currentDialog.textDialog);

                                  if (randAccStream != null)
                                  {
                                      // Load the file into the Document property of the RichEditBox.
                                      richEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                                  }
                              }
                              else
                              {
                                  await new Windows.UI.Popups.MessageDialog("It's the page Max").ShowAsync();
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
        /// 获取日志
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandGetDialog
        {
            get { return _CommandGetDialogLocator(this).Value; }
            set { _CommandGetDialogLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGetDialog Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGetDialog = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetDialogLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetDialogLocator = RegisterContainerLocator("CommandGetDialog", m => m.Initialize("CommandGetDialog", ref m._CommandGetDialog, ref _CommandGetDialogLocator,
              model =>
              {
                  var state = "CommandGetDialog";
                  var commandId = "CommandGetDialog";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add GetDialog logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;

                              Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                        await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream(vm.currentDialog.textDialog);

                              if (randAccStream != null)
                              {
                                  // Load the file into the Document property of the RichEditBox.
                                  richEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
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
        /// 删除日志
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandDeleteDialog
        {
            get { return _CommandDeleteDialogLocator(this).Value; }
            set { _CommandDeleteDialogLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandDeleteDialog Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandDeleteDialog = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandDeleteDialogLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandDeleteDialogLocator = RegisterContainerLocator("CommandDeleteDialog", m => m.Initialize("CommandDeleteDialog", ref m._CommandDeleteDialog, ref _CommandDeleteDialogLocator,
              model =>
              {
                  var state = "CommandDeleteDialog";
                  var commandId = "CommandDeleteDialog";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add DeleteDialog logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              //获取数据库服务
                              DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
                              dataService.DeleteDialog(vm.currentDialog.idDialog);
                              vm.listDialog.Remove(vm.currentDialog);
                              if (vm.listDialog.Count == 0)
                              {
                                  vm.currentDialog.textDialog = "default.rtf";
                                  vm.currentDialog.timeDialog = "null";
                                  vm.currentDialog.describeDialog = "null";
                              }
                              else
                              {
                                  if (vm.indexDialog == 0)
                                  {
                                      vm.currentDialog = vm.listDialog[0];
                                  }
                                  else
                                  {
                                      --vm.indexDialog;
                                      vm.currentDialog = vm.listDialog[vm.indexDialog];
                                  }
                                  
                              }
                              Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                    await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream(vm.currentDialog.textDialog);

                              if (randAccStream != null)
                              {
                                  // Load the file into the Document property of the RichEditBox.
                                  vm.editBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);

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

        ///// <summary>
        ///// This will be invoked by view when the view fires Load event and this viewmodel instance is already in view's ViewModel property
        ///// </summary>
        ///// <param name="view">View that firing Load event</param>
        ///// <returns>Task awaiter</returns>
        protected  async override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            
            //获取当前dialog的index以便进行上一篇下一篇
            if (currentDialog != null && listDialog != null)
            {
                for(int i = 0; i < listDialog.Count; ++i)
                {
                    if(listDialog[i].Equals(currentDialog))
                    {
                        indexDialog = i;
                        break;
                    }
                }
                Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                    await ServiceLocator.Instance.Resolve<FileService>().GetRandomAccessStream(currentDialog.textDialog);

                if (randAccStream != null)
                {
                    // Load the file into the Document property of the RichEditBox.
                    editBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);
                    
                }
            }

            await base.OnBindedViewLoad(view);
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

