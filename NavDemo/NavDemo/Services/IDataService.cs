using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public interface IDataService
    {
        /// <summary>
        /// 插入Friend到数据库（用于测试）
        /// </summary>
        /// <returns></returns>
        int InsertFriend();
        /// <summary>
        /// 插入Friend到数据库库
        /// </summary>
        /// <param name="friend">要插入的Friend</param>
        /// <returns></returns>
        int InsertFriend(Friend friend);
        /// <summary>
        /// 从数据库删除Friend
        /// </summary>
        /// <param name="idFriend">要删除Friend的id</param>
        void DeleteFriend(int idFriend);
        /// <summary>
        /// 从数据库获取所有Friend
        /// </summary>
        /// <returns>所有Friend</returns>
        List<Friend> GetAllFriends();
        /// <summary>
        /// 获取特定id的Friend
        /// </summary>
        /// <param name="id">Friend的id</param>
        /// <returns>所有的指定id的Friend</returns>
        List<Friend> GetFriends(int id);
        /// <summary>
        /// 插入Dialog到数据库（用于测试）
        /// </summary>
        /// <returns></returns>
        int InsertDialog();
        /// <summary>
        /// 插入dialog到数据库
        /// </summary>
        /// <param name="dialog">要插入的日志</param>
        /// <returns></returns>
        int InsertDialog(Dialog dialog);
        /// <summary>
        /// 从数据库删除指定id的Dialog
        /// </summary>
        /// <param name="idDialog">要删除的Dialog的id</param>
        void DeleteDialog(int idDialog);

        /// <summary>
        /// 获取数据库中最后一篇Dialog的id
        /// </summary>
        /// <returns>最后一篇Dialog的id</returns>
        int GetLastDialogId();

        /// <summary>
        /// 获取数据中所有的Dialog
        /// </summary>
        /// <returns>所有的Dialog</returns>
        List<Dialog> GetAllDialogs();
        /// <summary>
        /// 获取指定Friend id的Dialog(按时间降序)
        /// </summary>
        /// <param name="idFriend">与Dialog相关联的Friend的id</param>
        /// <returns></returns>
        List<Dialog> GetDialogs(int idFriend);
        /// <summary>
        /// 获取指定时间的Dialog
        /// </summary>
        /// <param name="timeDialog">某个Dialog的相应时间</param>
        /// <returns></returns>
        List<Dialog> GetDialogs(string timeDialog);
    }
}

