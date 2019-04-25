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
using Windows.UI.Xaml.Media;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Diagnostics;
using Windows.System;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Media.Imaging;

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

        /// <summary>
        /// 当前正在编辑的dialog
        /// </summary>
        public Dialog currentDialog { get => _currentDialogLocator(this).Value; set => _currentDialogLocator(this).SetValueAndTryNotify(value); }
        #region Property Dialog currentDialog Setup        
        protected Property<Dialog> _currentDialog = new Property<Dialog> { LocatorFunc = _currentDialogLocator };
        static Func<BindableBase, ValueContainer<Dialog>> _currentDialogLocator = RegisterContainerLocator(nameof(currentDialog), m => m.Initialize(nameof(currentDialog), ref m._currentDialog, ref _currentDialogLocator, () => default(Dialog)));
        #endregion

        /// <summary>
        /// 目标朋友
        /// </summary>
        public Friend chosenFriend { get => _chosenFriendLocator(this).Value; set => _chosenFriendLocator(this).SetValueAndTryNotify(value); }
        #region Property Friend chosenFriend Setup        
        protected Property<Friend> _chosenFriend = new Property<Friend> { LocatorFunc = _chosenFriendLocator };
        static Func<BindableBase, ValueContainer<Friend>> _chosenFriendLocator = RegisterContainerLocator(nameof(chosenFriend), m => m.Initialize(nameof(chosenFriend), ref m._chosenFriend, ref _chosenFriendLocator, () => default(Friend)));
        #endregion

        /// <summary>
        /// 朋友列表
        /// </summary>
        public List<Friend> friendItemList { get => _friendItemListLocator(this).Value; set => _friendItemListLocator(this).SetValueAndTryNotify(value); }
        #region Property List<Friend> friendItemList Setup        
        protected Property<List<Friend>> _friendItemList = new Property<List<Friend>> { LocatorFunc = _friendItemListLocator };
        static Func<BindableBase, ValueContainer<List<Friend>>> _friendItemListLocator = RegisterContainerLocator(nameof(friendItemList), m => m.Initialize(nameof(friendItemList), ref m._friendItemList, ref _friendItemListLocator, () => default(List<Friend>)));
        #endregion


        /// <summary>
        /// Suggest服务，用来生成提示项
        /// </summary>
        public SuggestService suggest { get => _suggestLocator(this).Value; set => _suggestLocator(this).SetValueAndTryNotify(value); }
        #region Property SuggestService suggest Setup        
        protected Property<SuggestService> _suggest = new Property<SuggestService> { LocatorFunc = _suggestLocator };
        static Func<BindableBase, ValueContainer<SuggestService>> _suggestLocator = RegisterContainerLocator(nameof(suggest), m => m.Initialize(nameof(suggest), ref m._suggest, ref _suggestLocator, () => default(SuggestService)));
        #endregion

        /// <summary>
        /// 用来初始化富文本框的字体类型
        /// </summary>
        public FontFamily fontFamily { get => _fontFamilyLocator(this).Value; set => _fontFamilyLocator(this).SetValueAndTryNotify(value); }
        #region Property FontFamily fontFamily Setup        
        protected Property<FontFamily> _fontFamily = new Property<FontFamily> {LocatorFunc = _fontFamilyLocator };
        static Func<BindableBase, ValueContainer<FontFamily>> _fontFamilyLocator = RegisterContainerLocator(nameof(fontFamily), m => m.Initialize(nameof(fontFamily), ref m._fontFamily, ref _fontFamilyLocator, () => default(FontFamily)));
        #endregion

        /// <summary>
        /// SuggestBox的当前Text内容
        /// </summary>
        public string suggestBoxText { get => _suggestBoxTextLocator(this).Value; set => _suggestBoxTextLocator(this).SetValueAndTryNotify(value); }
        #region Property string suggestBoxText Setup        
        protected Property<string> _suggestBoxText = new Property<string> { LocatorFunc = _suggestBoxTextLocator };
        static Func<BindableBase, ValueContainer<string>> _suggestBoxTextLocator = RegisterContainerLocator(nameof(suggestBoxText), m => m.Initialize(nameof(suggestBoxText), ref m._suggestBoxText, ref _suggestBoxTextLocator, () => default(string)));
        #endregion



        /// <summary>
        /// 数据库中最后一篇文章的id号+1，也就是新建的dialog的编号
        /// </summary>
        public int currentIndex { get => _currentIndexLocator(this).Value; set => _currentIndexLocator(this).SetValueAndTryNotify(value); }
        #region Property int currentIndex Setup        
        protected Property<int> _currentIndex = new Property<int> {LocatorFunc = _currentIndexLocator };
        static Func<BindableBase, ValueContainer<int>> _currentIndexLocator = RegisterContainerLocator(nameof(currentIndex), m => m.Initialize(nameof(currentIndex), ref m._currentIndex, ref _currentIndexLocator, () => default(int)));
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
                              vm.currentDialog.idFriend = vm.chosenFriend.idFriend;
                              vm.currentDialog.nickNameFriend = vm.chosenFriend.nickNameFriend;
                              vm.currentDialog.nameFriend = vm.chosenFriend.nameFriend;
                              await new Windows.UI.Popups.MessageDialog(vm.chosenFriend.idFriend.ToString()).ShowAsync();
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
        /// 插入Dialog
        /// </summary>
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


                              vm.currentDialog.textDialog = vm.currentIndex.ToString() + ".rtf";
                              ServiceLocator.Instance.Resolve<DataService>()
                                 .InsertDialog(vm.currentDialog);
                              vm.currentIndex++;
                              StringBuilder sb = new StringBuilder();
                              DataService dataService = ServiceLocator.Instance.Resolve<DataService>();

                              List<Dialog> list = dataService.GetAllDialogs();
                              foreach (Dialog item in list)
                              {
                                  sb.AppendLine($"{item.idFriend} {item.timeDialog} {item.describeDialog} {item.textDialog} ");
                              }
                              await new Windows.UI.Popups.MessageDialog(sb.ToString()).ShowAsync();

                              //ExportFile Service
                              Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
                              savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

                              // Dropdown of file types the user can save the file as
                              savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

                              // Default file name if the user does not type one in or select a file to replace
                              savePicker.SuggestedFileName = "New Document";

                              Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                              string fileName = vm.currentDialog.textDialog;
                              Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

                              await new Windows.UI.Popups.MessageDialog(fileName).ShowAsync();
                              if (file != null)
                              {
                                  // Prevent updates to the remote version of the file until we
                                  // finish making changes and call CompleteUpdatesAsync.
                                  Windows.Storage.CachedFileManager.DeferUpdates(file);
                                  // write to file
                                  Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                      await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                                  RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;
                                  richEditBox.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);

                                  // Let Windows know that we're finished changing the file so the
                                  // other app can update the remote version of the file.
                                  Windows.Storage.Provider.FileUpdateStatus status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                                  if (status != Windows.Storage.Provider.FileUpdateStatus.Complete)
                                  {
                                      Windows.UI.Popups.MessageDialog errorBox =
                                          new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.");
                                      await errorBox.ShowAsync();
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
        /// 通过点击日历获取日期
        /// </summary>
        public CommandModel<ReactiveCommand, String> CommandChangeDate
        {
            get { return _CommandChangeDateLocator(this).Value; }
            set { _CommandChangeDateLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChangeDate Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChangeDate = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChangeDateLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChangeDateLocator = RegisterContainerLocator("CommandChangeDate", m => m.Initialize("CommandChangeDate", ref m._CommandChangeDate, ref _CommandChangeDateLocator,
              model =>
              {
                  var state = "CommandChangeDate";
                  var commandId = "CommandChangeDate";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                              //Todo: Add ChangeDate logic here, or
                              await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              CalendarDatePickerDateChangedEventArgs arg = (CalendarDatePickerDateChangedEventArgs)e.EventArgs.Parameter;
                              var date = arg.NewDate.Value;
                              StringBuilder sb = new StringBuilder();
                              vm.currentDialog.timeDialog = "";
                              vm.currentDialog.timeDialog += date.Year.ToString() + '/' + date.Month.ToString() + '/' + date.Day.ToString();
                              vm.currentDialog.flagTime = date.Year * 365 + date.Month * 30 + date.Day;






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
            fontFamily = new FontFamily("SongTi");
            suggestBoxText = ""; 
            //获取数据库服务
            DataService dataService = ServiceLocator.Instance.Resolve<DataService>();
            //获取当前最后一篇dialog的id
            currentIndex = dataService.GetLastDialogId();
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
        protected override Task OnBindedViewUnload(MVVMSidekick.Views.IView view)
        {
            friendItemList.Clear();
            suggestBoxText = "";
            return base.OnBindedViewUnload(view);
        }

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
        /// SuggestBox 建议项的点击监听器
        /// </summary>
        private void TextChangedCommand()
        {
            //一般列表项点击事件
            MVVMSidekick.EventRouting.EventRouter.Instance.GetEventChannel<Object>()
                .Where(x => x.EventName == "TextChangedEventRouter")
                     .Subscribe(
                          e =>
                          {                            
                              friendItemList = ServiceLocator.Instance.Resolve<SuggestService>().Suggest(suggestBoxText);
                          }
                     ).DisposeWith(this);
        }
    }

}

