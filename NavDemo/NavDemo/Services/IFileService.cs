using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public interface IFileService
    {
        /// <summary>
        /// 通过FilePicker获取富文本流（弹出框选文件）
        /// </summary>
        /// <returns>富文本流</returns>
        Task<Windows.Storage.Streams.IRandomAccessStream> GetRandomAccessStream();

        /// <summary>
        /// 设置夫文本框的内容流
        /// </summary>
        /// <param name="editor">富文本框</param>
        Task SetRandomAccessStream(Windows.UI.Xaml.Controls.RichEditBox editor);

        /// <summary>
        /// 从文件中获取富文本流
        /// </summary>
        /// <param name="fileName">流来源的文件名</param>
        /// <returns>富文本流</returns>
        Task<Windows.Storage.Streams.IRandomAccessStream> GetRandomAccessStream(string fileName);

        /// <summary>
        /// 将好友信息从文件读入到数据库
        /// </summary>
        /// <param name="fileName">Friend来源文件</param>
        Task ReadFriend(string fileName);

        /// <summary>
        /// 将日志信息从文件读入到数据库
        /// </summary>
        /// <param name="fileName">Friend来源文件</param>
        Task ReadDialog(string fileName);

        /// <summary>
        /// 将好友信息从数据库写入到文件
        /// </summary>
        /// <param name="fileName">Friend来源文件</param>
        Task WriteFriend(string fileName);

        /// <summary>
        /// 将日志信息从数据库写入到文件
        /// </summary>
        /// <param name="fileName">Friend来源文件</param>
        Task WriteDialog(string fileName);
    }
}
