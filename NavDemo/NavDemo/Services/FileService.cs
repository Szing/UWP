using MVVMSidekick.Services;
using NavDemo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace NavDemo.Services
{

    public class FileService : Singleton<FileService>
    {

        public async Task<IRandomAccessStream> GetRandomAccessStream()
        {
            Windows.Storage.Pickers.FileOpenPicker open =
                new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");

            Windows.Storage.StorageFile file = await open.PickSingleFileAsync();

            if (file != null)
            {
                try
                {
                    Windows.Storage.Streams.IRandomAccessStream randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    // Load the file into the Document property of the RichEditBox.
                    return randAccStream;
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
            return null;
        }

        public async Task SetRandomAccessStream(RichEditBox editor)
        {
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
                editor.Document.SaveToStream(Windows.UI.Text.TextGetOptions.FormatRtf, randAccStream);


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
        }

        public async Task<IRandomAccessStream> GetRandomAccessStream(string fileName)
        {
            Windows.Storage.Pickers.FileOpenPicker open =
                new Windows.Storage.Pickers.FileOpenPicker();
            open.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            
            StorageFile file = await storageFolder.GetFileAsync(fileName);

            
            if (file != null)
            {
                try
                {
                    Windows.Storage.Streams.IRandomAccessStream randAccStream =
                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    // Load the file into the Document property of the RichEditBox.
                    return randAccStream;
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
            return null;
        }

        public async Task ReadFriend(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            StorageFile file = await storageFolder.GetFileAsync(fileName);
            string[] strs = File.ReadAllLines(file.Path);
            foreach(var i in strs)
            {
                Debug.WriteLine(i.ToString());
                if (i[0] < '0' || i[0] > '9')
                    break;
                int tag = 0;
                string id = "";
                int id1;
                string name = "";
                string nickname = "";
                string icon = "";
                for(int k = 0; k < i.Length; ++k)
                {
                    if (i[k] == ',')
                    {
                        tag++;
                        if (tag == 4)
                            break;
                        continue;
                    }
                       
                    switch(tag)
                    {
                        case 0:
                            id += i[k];
                            break;
                        case 1:
                            name += i[k];
                            break;
                        case 2:
                            icon += i[k];
                            break;
                        case 3:
                            nickname += i[k];
                            break;
                        default:
                            break;
                    }
                    
                }
                id1 = int.Parse(id);
                Debug.WriteLine(id1.ToString()+" "+name+" "+icon+" "+nickname);
            }

        }
        public async Task ReadDialog(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            StorageFile file = await storageFolder.GetFileAsync(fileName);
            string[] strs = File.ReadAllLines(file.Path);
            foreach (var i in strs)
            {
                if (i == null)
                    break;
                Debug.WriteLine(i.ToString());
                
                if (i[0] < '0' || i[0] > '9')
                    break;
                int tag = 0;
                string idD = "";
                string idF = "";
                string flag = "";
                int id1;
                int id2;
                int id3;
                string name = "";
                string nickname = "";
                string img = "";
                string des = "";
                string text = "";
                string time = "";

                for (int k = 0; k < i.Length; ++k)
                {
                    if (i[k] == ',')
                    {
                        tag++;
                        if (tag == 9)
                            break;
                        continue;
                    }

                    switch (tag)
                    {
                        case 0:
                            idD += i[k];
                            break;
                        case 1:
                            idF += i[k];
                            break;
                        case 2:
                            name += i[k];
                            break;
                        case 3:
                            nickname += i[k];;
                            break;
                        case 4:
                            img += i[k];
                            break;
                        case 5:
                            des += i[k];
                            break;
                        case 6:
                            text += i[k];
                            break;
                        case 7:
                            time += i[k];
                            break;
                        case 8:
                            flag += i[k];
                            break;
                        default:
                            break;
                    }

                }
                id1 = int.Parse(idD);
                id2 = int.Parse(idF);
                id3 = int.Parse(flag);
                Debug.WriteLine(id1.ToString() + " " + id2.ToString() +" "+ name + " "  + nickname +" "+ img + " "+des +" "+text+" "+time+" "+ id3.ToString());
                Dialog tempDialog = new Dialog();
                tempDialog.Set(id1, id2, name, nickname, img, des, text, time, id3);
                ServiceLocator.Instance.Resolve<DataService>().InsertDialog(tempDialog);
            }

        }
        public async Task WriteFriend(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            StorageFile file = await storageFolder.GetFileAsync(fileName);
            List<Friend> listFriend  = ServiceLocator.Instance.Resolve<DataService>().GetAllFriends();
            
            List<string> list  = new List<string> ();
            foreach(var i in listFriend)
            {
                string friend = "";
                friend += i.idFriend.ToString() + ',' +i.nameFriend + ',' + i.iconFriend + ',' + i.nickNameFriend + ',';
                list.Add(friend);
            }

            File.WriteAllLines(file.Path,list.ToArray());
        }
        public async Task WriteDialog(string fileName)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            StorageFile file = await storageFolder.GetFileAsync(fileName);
            List<Dialog> listDialog = ServiceLocator.Instance.Resolve<DataService>().GetAllDialogs();

            List<string> list = new List<string>();
            foreach (var i in listDialog)
            {
                string dialog = "";
                dialog += i.idDialog.ToString() + ',' + i.idFriend.ToString() + ',' + i.nameFriend + ',' 
                    + i.nickNameFriend + ',' + i.imageDialog + ','+ i.describeDialog + ','
                    + i.textDialog + ',' + i.timeDialog + ',' + i.flagTime.ToString() + ',';
                list.Add(dialog);
            }

            File.WriteAllLines(file.Path, list.ToArray());
        }
    }
}
