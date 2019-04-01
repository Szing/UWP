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


        public Dialog currentDialog { get => _currentDialogLocator(this).Value; set => _currentDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property Dialog currentDialog Setup        
        protected Property<Dialog> _currentDialog = new Property<Dialog> { LocatorFunc = _currentDialogLocator };
        static Func<BindableBase, ValueContainer<Dialog>> _currentDialogLocator = RegisterContainerLocator(nameof(currentDialog), m => m.Initialize(nameof(currentDialog), ref m._currentDialog, ref _currentDialogLocator, () => default(Dialog)));
        #endregion

        public List<Dialog> listDialog { get => _listDialogLocator(this).Value; set => _listDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Dialog> listDialog Setup        
        protected Property<List<Dialog>> _listDialog = new Property<List<Dialog>> { LocatorFunc = _listDialogLocator };
        static Func<BindableBase, ValueContainer<List<Dialog>>> _listDialogLocator = RegisterContainerLocator(nameof(listDialog), m => m.Initialize(nameof(listDialog), ref m._listDialog, ref _listDialogLocator, () => default(List<Dialog>)));
        #endregion


        public int indexDialog { get => _indexDialogLocator(this).Value; set => _indexDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property int indexDialog Setup        
        protected Property<int> _indexDialog = new Property<int> { LocatorFunc = _indexDialogLocator };
        static Func<BindableBase, ValueContainer<int>> _indexDialogLocator = RegisterContainerLocator(nameof(indexDialog), m => m.Initialize(nameof(indexDialog), ref m._indexDialog, ref _indexDialogLocator, () => default(int)));
        #endregion


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
                              Windows.Storage.Pickers.FileOpenPicker open = new Windows.Storage.Pickers.FileOpenPicker();
                              open.SuggestedStartLocation =
                                  Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                              open.FileTypeFilter.Add(".rtf");

                              //用于获取存放日志信息的目录
                              //String path = ApplicationData.Current.LocalFolder.Path; 

                              //用路径信息获取文件
                              StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                              string fileName = vm.currentDialog.textDialog;
                              StorageFile file = await storageFolder.GetFileAsync(fileName);
                             
                              
                              if (file != null)
                              {
                                  try
                                  {
                                      Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                  await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                                      // Load the file into the Document property of the RichEditBox.
                                      RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;
                                      richEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);

                                      
                                  }
                                  catch (Exception)
                                  {
                                      ContentDialog errorDialog = new ContentDialog()
                                      {
                                          Title = "File open error",
                                          Content = "Sorry, I couldn't open the file.",
                                          PrimaryButtonText = "Ok"
                                      };

                                      await errorDialog.ShowAsync();
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
        protected override Task OnBindedViewLoad(MVVMSidekick.Views.IView view)
        {
            //获取当前dialog的index以便进行上一篇下一篇
            if(currentDialog != null && listDialog != null)
            {
                for(int i = 0; i < listDialog.Count(); ++i)
                {
                    if(listDialog[i].Equals(currentDialog))
                    {
                        indexDialog = i;
                    }
                }
            }

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

