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


        public FontFamily fontFamily { get => _fontFamilyLocator(this).Value; set => _fontFamilyLocator(this).SetValueAndTryNotify(value); }
        #region Property FontFamily fontFamily Setup        
        protected Property<FontFamily> _fontFamily = new Property<FontFamily> {LocatorFunc = _fontFamilyLocator };
        static Func<BindableBase, ValueContainer<FontFamily>> _fontFamilyLocator = RegisterContainerLocator(nameof(fontFamily), m => m.Initialize(nameof(fontFamily), ref m._fontFamily, ref _fontFamilyLocator, () => default(FontFamily)));
        #endregion



        public RichEditBox currentRichEditBox { get => _currentRichEditBoxLocator(this).Value; set => _currentRichEditBoxLocator(this).SetValueAndTryNotify(value); }
        #region Property RichEditBox currentRichEditBox Setup        
        protected Property<RichEditBox> _currentRichEditBox = new Property<RichEditBox> { LocatorFunc = _currentRichEditBoxLocator };
        static Func<BindableBase, ValueContainer<RichEditBox>> _currentRichEditBoxLocator = RegisterContainerLocator(nameof(currentRichEditBox), m => m.Initialize(nameof(currentRichEditBox), ref m._currentRichEditBox, ref _currentRichEditBoxLocator, () => default(RichEditBox)));
        #endregion



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


        public CommandModel<ReactiveCommand, String> CommandImportDialog
        {
            get { return _CommandImportDialogLocator(this).Value; }
            set { _CommandImportDialogLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandImportDialog Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandImportDialog = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandImportDialogLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandImportDialogLocator = RegisterContainerLocator("CommandImportDialog", m => m.Initialize("CommandImportDialog", ref m._CommandImportDialog, ref _CommandImportDialogLocator,
              model =>
              {
                  var state = "CommandImportDialog";
                  var commandId = "CommandImportDialog";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ImportDialog logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              Windows.Storage.Pickers.FileOpenPicker open = new Windows.Storage.Pickers.FileOpenPicker();
                              open.SuggestedStartLocation =
                                  Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                              open.FileTypeFilter.Add(".rtf");

                              //用于获取存放日志信息的目录
                              //String path = ApplicationData.Current.LocalFolder.Path; 

                              //用路径信息获取文件
                              //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                              //StorageFile file = await storageFolder.GetFileAsync("New Document.rtf");

                              // Create sample file; replace if exists.
                              Windows.Storage.StorageFile file = await open.PickSingleFileAsync();

                              if (file != null)
                              {
                                  try
                                  {
                                      Windows.Storage.Streams.IRandomAccessStream randAccStream =
                                  await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                                      // Load the file into the Document property of the RichEditBox.
                                      RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;
                                      richEditBox.Document.LoadFromStream(Windows.UI.Text.TextSetOptions.FormatRtf, randAccStream);

                                      await new Windows.UI.Popups.MessageDialog("successful").ShowAsync();
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


        public CommandModel<ReactiveCommand, String> CommandExportDialog
        {
            get { return _CommandExportDialogLocator(this).Value; }
            set { _CommandExportDialogLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandExportDialog Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandExportDialog = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandExportDialogLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandExportDialogLocator = RegisterContainerLocator("CommandExportDialog", m => m.Initialize("CommandExportDialog", ref m._CommandExportDialog, ref _CommandExportDialogLocator,
              model =>
              {
                  var state = "CommandExportDialog";
                  var commandId = "CommandExportDialog";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ExportDialog logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker();
                              savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

                              // Dropdown of file types the user can save the file as
                              savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

                              // Default file name if the user does not type one in or select a file to replace
                              savePicker.SuggestedFileName = "New Document";

                              Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                              
                              
                             
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


        public CommandModel<ReactiveCommand, String> CommandChoseFontFamily
        {
            get { return _CommandChoseFontFamilyLocator(this).Value; }
            set { _CommandChoseFontFamilyLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontFamily Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontFamily = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontFamilyLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontFamilyLocator = RegisterContainerLocator("CommandChoseFontFamily", m => m.Initialize("CommandChoseFontFamily", ref m._CommandChoseFontFamily, ref _CommandChoseFontFamilyLocator,
              model =>
              {
                  var state = "CommandChoseFontFamily";
                  var commandId = "CommandChoseFontFamily";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontFamily logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              var parameter = e.EventArgs.Parameter as Windows.UI.Xaml.Input.TappedRoutedEventArgs;
                              var textBlock = parameter.OriginalSource as TextBlock;
                              vm.fontFamily = textBlock.FontFamily;
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


        public CommandModel<ReactiveCommand, String> CommandChoseFontSize
        {
            get { return _CommandChoseFontSizeLocator(this).Value; }
            set { _CommandChoseFontSizeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontSize Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontSize = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontSizeLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontSizeLocator = RegisterContainerLocator("CommandChoseFontSize", m => m.Initialize("CommandChoseFontSize", ref m._CommandChoseFontSize, ref _CommandChoseFontSizeLocator,
              model =>
              {
                  var state = "CommandChoseFontSize";
                  var commandId = "CommandChoseFontSize";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontSize logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              var i = e;
                              var parameter = e.EventArgs.Parameter as  Windows.UI.Xaml.Input.TappedRoutedEventArgs;
                              var textBlock = parameter.OriginalSource as TextBlock;
                              // 获取字号
                              float size = Convert.ToSingle(textBlock.FontSize);

                              vm.currentRichEditBox.Document.Selection.CharacterFormat.Size = size;
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


        public CommandModel<ReactiveCommand, String> CommandChoseFontColor
        {
            get { return _CommandChoseFontColorLocator(this).Value; }
            set { _CommandChoseFontColorLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontColor Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontColor = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontColorLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontColorLocator = RegisterContainerLocator("CommandChoseFontColor", m => m.Initialize("CommandChoseFontColor", ref m._CommandChoseFontColor, ref _CommandChoseFontColorLocator,
              model =>
              {
                  var state = "CommandChoseFontColor";
                  var commandId = "CommandChoseFontColor";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontColor logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              //var i = e;
                              var parameter = e.EventArgs.Parameter as Windows.UI.Xaml.RoutedEventArgs;
                              var clickedColor = parameter.OriginalSource as Windows.UI.Xaml.Controls.Button;
                              var rectangle = (Windows.UI.Xaml.Shapes.Rectangle)clickedColor.Content;
                              var color = ((Windows.UI.Xaml.Media.SolidColorBrush)rectangle.Fill).Color;

                              vm.currentRichEditBox.Document.Selection.CharacterFormat.ForegroundColor = color;

                              //fontColorButton.Flyout.Hide();
                              vm.currentRichEditBox.Focus(Windows.UI.Xaml.FocusState.Keyboard);
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



        public CommandModel<ReactiveCommand, String> CommandGetRichEditBox
        {
            get { return _CommandGetRichEditBoxLocator(this).Value; }
            set { _CommandGetRichEditBoxLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandGetRichEditBox Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandGetRichEditBox = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandGetRichEditBoxLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandGetRichEditBoxLocator = RegisterContainerLocator("CommandGetRichEditBox", m => m.Initialize("CommandGetRichEditBox", ref m._CommandGetRichEditBox, ref _CommandGetRichEditBoxLocator,
              model =>
              {
                  var state = "CommandGetRichEditBox";
                  var commandId = "CommandGetRichEditBox";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add GetRichEditBox logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              //var i = e;
                              var parameter = e.EventArgs.Parameter as Windows.UI.Xaml.RoutedEventArgs;
                              var button = parameter.OriginalSource as Windows.UI.Xaml.Controls.AppBarButton;
                              vm.currentRichEditBox = button.Tag as Windows.UI.Xaml.Controls.RichEditBox;
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




        public CommandModel<ReactiveCommand, String> CommandChoseFontBold
        {
            get { return _CommandChoseFontBoldLocator(this).Value; }
            set { _CommandChoseFontBoldLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontBold Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontBold = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontBoldLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontBoldLocator = RegisterContainerLocator("CommandChoseFontBold", m => m.Initialize("CommandChoseFontBold", ref m._CommandChoseFontBold, ref _CommandChoseFontBoldLocator,
              model =>
              {
                  var state = "CommandChoseFontBold";
                  var commandId = "CommandChoseFontBold";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontBold logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              
                              var richEditBox = e.EventArgs.Parameter as Windows.UI.Xaml.Controls.RichEditBox;
                              Windows.UI.Text.ITextSelection selectedText = richEditBox.Document.Selection;
                              if (selectedText != null)
                              {
                                  Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                                  charFormatting.Bold = Windows.UI.Text.FormatEffect.Toggle;
                                  selectedText.CharacterFormat = charFormatting;
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




        public CommandModel<ReactiveCommand, String> CommandChoseFontItalic
        {
            get { return _CommandChoseFontItalicLocator(this).Value; }
            set { _CommandChoseFontItalicLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontItalic Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontItalic = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontItalicLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontItalicLocator = RegisterContainerLocator("CommandChoseFontItalic", m => m.Initialize("CommandChoseFontItalic", ref m._CommandChoseFontItalic, ref _CommandChoseFontItalicLocator,
              model =>
              {
                  var state = "CommandChoseFontItalic";
                  var commandId = "CommandChoseFontItalic";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontItalic logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              var richEditBox = e.EventArgs.Parameter as Windows.UI.Xaml.Controls.RichEditBox;
                              Windows.UI.Text.ITextSelection selectedText = richEditBox.Document.Selection;
                              if (selectedText != null)
                              {
                                  Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                                  charFormatting.Italic = Windows.UI.Text.FormatEffect.Toggle;
                                  selectedText.CharacterFormat = charFormatting;
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


        public CommandModel<ReactiveCommand, String> CommandChoseFontUnderline
        {
            get { return _CommandChoseFontUnderlineLocator(this).Value; }
            set { _CommandChoseFontUnderlineLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandChoseFontUnderline Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandChoseFontUnderline = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandChoseFontUnderlineLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandChoseFontUnderlineLocator = RegisterContainerLocator("CommandChoseFontUnderline", m => m.Initialize("CommandChoseFontUnderline", ref m._CommandChoseFontUnderline, ref _CommandChoseFontUnderlineLocator,
              model =>
              {
                  var state = "CommandChoseFontUnderline";
                  var commandId = "CommandChoseFontUnderline";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add ChoseFontUnderline logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              var richEditBox = e.EventArgs.Parameter as Windows.UI.Xaml.Controls.RichEditBox;
                              Windows.UI.Text.ITextSelection selectedText = richEditBox.Document.Selection;
                              if (selectedText != null)
                              {
                                  Windows.UI.Text.ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                                  if (charFormatting.Underline == Windows.UI.Text.UnderlineType.None)
                                  {
                                      charFormatting.Underline = Windows.UI.Text.UnderlineType.Single;
                                  }
                                  else
                                  {
                                      charFormatting.Underline = Windows.UI.Text.UnderlineType.None;
                                  }
                                  selectedText.CharacterFormat = charFormatting;
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



        public CommandModel<ReactiveCommand, String> CommandInsertImage
        {
            get { return _CommandInsertImageLocator(this).Value; }
            set { _CommandInsertImageLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property CommandModel<ReactiveCommand, String> CommandInsertImage Setup               
        protected Property<CommandModel<ReactiveCommand, String>> _CommandInsertImage = new Property<CommandModel<ReactiveCommand, String>> { LocatorFunc = _CommandInsertImageLocator };
        static Func<BindableBase, ValueContainer<CommandModel<ReactiveCommand, String>>> _CommandInsertImageLocator = RegisterContainerLocator("CommandInsertImage", m => m.Initialize("CommandInsertImage", ref m._CommandInsertImage, ref _CommandInsertImageLocator,
              model =>
              {
                  var state = "CommandInsertImage";
                  var commandId = "CommandInsertImage";
                  var vm = CastToCurrentType(model);
                  var cmd = new ReactiveCommand(canExecute: true) { ViewModel = model };

                  cmd.DoExecuteUIBusyTask(
                          vm,
                          async e =>
                          {
                            //Todo: Add InsertImage logic here, or
                            await MVVMSidekick.Utilities.TaskExHelper.Yield();
                              RichEditBox richEditBox = (RichEditBox)e.EventArgs.Parameter;

                              var picker = new Windows.Storage.Pickers.FileOpenPicker();

                              picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail; //可通过使用图片缩略图创建丰富的视觉显示，以显示文件选取器中的文件

                              picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

                              picker.FileTypeFilter.Add(".jpg");

                              picker.FileTypeFilter.Add(".jpeg");

                              picker.FileTypeFilter.Add(".png");

                              picker.FileTypeFilter.Add(".gif");



                              //选取单个文件

                              Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();



                              //文件处理

                              if (file != null)

                              {

                                  var inputFile = SharedStorageAccessManager.AddFile(file);

                                  var destination = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cropped.jpg", CreationCollisionOption.ReplaceExisting);//在应用文件夹中建立文件用来存储裁剪后的图像

                                  var destinationFile = SharedStorageAccessManager.AddFile(destination);

                                  var options = new LauncherOptions();

                                  options.TargetApplicationPackageFamilyName = "Microsoft.Windows.Photos_8wekyb3d8bbwe";



                                  //待会要传入的参数

                                  var parameters = new ValueSet();

                                  parameters.Add("InputToken", inputFile);                //输入文件

                                  parameters.Add("DestinationToken", destinationFile);    //输出文件

                                  parameters.Add("ShowCamera", false);                    //它允许我们显示一个按钮，以允许用户采取当场图象(但是好像并没有什么卵用)

                                  parameters.Add("EllipticalCrop", true);                 //截图区域显示为圆（最后截出来还是方形）

                                  parameters.Add("CropWidthPixals", 300);

                                  parameters.Add("CropHeightPixals", 300);



                                  //调用系统自带截图并返回结果

                                  var result = await Launcher.LaunchUriForResultsAsync(new Uri("microsoft.windows.photos.crop:"), options, parameters);



                                  //按理说下面这个判断应该没问题呀，但是如果裁剪界面点了取消的话后面会出现异常，所以后面我加了try catch

                                  if (result.Status == LaunchUriStatus.Success && result.Result != null)

                                  {

                                      //对裁剪后图像的下一步处理

                                      try

                                      {

                                          // 载入已保存的裁剪后图片

                                          

                                          var stream = await destination.OpenReadAsync();


                                         

                                          // 插入图片

                                          richEditBox.Document.Selection.InsertImage(200, 100, 0, Windows.UI.Text.VerticalCharacterAlignment.Baseline, "图像", stream);

                                         

                                      }

                                      catch (Exception ex)

                                      {

                                          Debug.WriteLine(ex.Message + ex.StackTrace);

                                      }

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

