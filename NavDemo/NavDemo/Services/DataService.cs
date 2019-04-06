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
    public class DataService : Singleton<DataService>
    {
       
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
        public int InsertFriend(Friend friend)
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
               
                int n = db.Insert(friend);
                
            }
            return result;
        }
        public void DeleteFriend(int idFriend)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                db.Delete<Friend>(idFriend);
            }
        }
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
        public int InsertDialog(Dialog dialog)
        {
            int result = 0;
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {

                int n = db.Insert(dialog);

            }
            return result;
        }
        public void DeleteDialog(int idDialog)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                db.Delete<Dialog>(idDialog);
            }
        }
        
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
        /// 获取降序排列的dialog
        /// </summary>
        /// <param name="idFriend">朋友id</param>
        /// <returns>该朋友对应listDialog</returns>
        public List<Dialog> GetDialogs(int idFriend)
        {
            using (var db = DbContext.GetInstance().GetSqLiteConnection())
            {
                string sql = "select *from tableDialog WHERE idFriend = ? ORDER BY timeDialog DESC";
                List<Dialog> list = new List<Dialog>();
                int temp = idFriend;
                list = db.Query<Dialog>(sql, temp);
                return list;

            }

        }
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
