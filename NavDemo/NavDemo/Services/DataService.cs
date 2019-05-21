using NavDemo.Models;
using NavDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Debug;
using SQLite.Net;
using System.Collections;
using NavDemo.Services;

namespace NavDemo.Services
{
    public class DataService : Singleton<DataService>,IDataService
    {

        /// <summary>
        /// 插入Friend到数据库（用于测试）
        /// </summary>
        /// <returns></returns>
        public int InsertFriend()
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                Friend[] stus =

                {

                    new Friend { nameFriend="小王",nickNameFriend = "1" },
                    new Friend { nameFriend="小丁",nickNameFriend = "1" },
                    new Friend { nameFriend="小赵",nickNameFriend = "1" },
                    new Friend { nameFriend="小马",nickNameFriend = "1" },
                    new Friend { nameFriend="小陈",nickNameFriend = "1" },
                    

                };
                int n = db.InsertAll(stus);
                WriteLine($"已插入 {n} 条数据。");
            }
            return result;
        }

        /// <summary>
        /// 插入Friend到数据库库
        /// </summary>
        /// <param name="friend">要插入的Friend</param>
        /// <returns></returns>
        public int InsertFriend(Friend friend)
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
               
                int n = db.Insert(friend);
                
            }
            return result;
        }

        /// <summary>
        /// 从数据库删除Friend
        /// </summary>
        /// <param name="idFriend">要删除Friend的id</param>
        public void DeleteFriend(int idFriend)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                db.Delete<Friend>(idFriend);
            }
        }
        /// <summary>
        /// 从数据库获取所有Friend
        /// </summary>
        /// <returns>所有Friend</returns>
        public List<Friend> GetAllFriends()
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                List<Friend> list = new List<Friend>();
                
                foreach (var item in db.Table<Friend>())
                {
                    list.Add(item);
                }

                return list;


            }
        }
        /// <summary>
        /// 获取特定id的Friend
        /// </summary>
        /// <param name="id">Friend的id</param>
        /// <returns>所有的指定id的Friend</returns>
        public List<Friend> GetFriends(int id)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                string sql = "select *from per_info WHERE id = ?";
                List<Friend> list = new List<Friend>();
                int temp = id;
                list = db.Query<Friend>(sql, temp);
                return list;

            }
            
        }
        /// <summary>
        /// 插入Dialog到数据库（用于测试）
        /// </summary>
        /// <returns></returns>
        public int InsertDialog()
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                Dialog[] stus =

                {

                    new Dialog { idFriend = 1, textDialog = "hello1" ,describeDialog = "first"},
                    new Dialog { idFriend = 2, textDialog = "hello2",describeDialog = "second"},
                    new Dialog { idFriend = 3, textDialog = "hello3",describeDialog = "third"},
                    new Dialog { idFriend = 4, textDialog = "hello4",describeDialog = "forth"},
                    new Dialog { idFriend = 5, textDialog = "hello5",describeDialog = "fifth"}


                };
                int n = db.InsertAll(stus);
                WriteLine($"已插入 {n} 条数据。");
            }
            return result;
        }
        /// <summary>
        /// 插入dialog到数据库
        /// </summary>
        /// <param name="dialog">要插入的日志</param>
        /// <returns></returns>
        public int InsertDialog(Dialog dialog)
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {

                int n = db.Insert(dialog);

            }
            return result;
        }
        /// <summary>
        /// 从数据库删除指定id的Dialog
        /// </summary>
        /// <param name="idDialog">要删除的Dialog的id</param>
        public void DeleteDialog(int idDialog)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                db.Delete<Dialog>(idDialog);
            }
        }

        /// <summary>
        /// 从数据库删除指定id的Dialog
        /// </summary>
        /// <param name="idFriend">要删除的Dialog属于的Friend的id</param>
        public void DeleteDialog(int idFriend,string name)
        {
            List<Dialog>  dialogs = GetDialogs(idFriend);
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                foreach(var d in dialogs)
                {
                    db.Delete<Dialog>(d.idDialog);
                }
                     
            }
        }

        /// <summary>
        /// 获取数据库中最后一篇Dialog的id
        /// </summary>
        /// <returns>最后一篇Dialog的id</returns>
        public int GetLastDialogId()
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                var list = db.Table<Dialog>();
                if (list.Count() == 0)
                    return 0;
                return list.ElementAt(list.Count() - 1).idDialog;
            }
        }

        private object ElementAt(int v)
        {
            throw new NotImplementedException();
        }
        

        /// <summary>
        /// 获取数据中所有的Dialog
        /// </summary>
        /// <returns>所有的Dialog</returns>
        public List<Dialog> GetAllDialogs()
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                List<Dialog> list = new List<Dialog>();
                foreach (var item in db.Table<Dialog>())
                {
                    list.Add(item);

                }

                return list;


            }
        }

        /// <summary>
        /// 获取指定Friend id的Dialog(按时间降序)
        /// </summary>
        /// <param name="idFriend">与Dialog相关联的Friend的id</param>
        /// <returns></returns>
        public List<Dialog> GetDialogs(int idFriend)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                string sql = "select *from tableDialog WHERE idFriend = ? ORDER BY flagTime DESC";
                List<Dialog> list = new List<Dialog>();
                int temp = idFriend;
                list = db.Query<Dialog>(sql, temp);
                return list;

            }

        }

        /// <summary>
        /// 获取指定时间的Dialog
        /// </summary>
        /// <param name="timeDialog">某个Dialog的相应时间</param>
        /// <returns></returns>
        public List<Dialog> GetDialogs(string timeDialog)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                string sql = "select *from tableDialog WHERE timeDialog = ?";
                List<Dialog> list = new List<Dialog>();
                list = db.Query<Dialog>(sql, timeDialog);
                return list;

            }

        }

        
    }
}
