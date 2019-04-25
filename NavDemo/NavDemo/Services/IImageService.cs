using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public interface IImageService
    {
        /// <summary>
        /// 获取经过截取后的图片流
        /// </summary>
        /// <returns>图片流</returns>
        Task<Windows.Storage.Streams.IRandomAccessStream> GetImageStream();
    }
}
