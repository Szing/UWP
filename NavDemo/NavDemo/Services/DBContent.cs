using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using NavDemo.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Windows.Storage;
using SQLite.Net.Interop;
using static System.Diagnostics.Debug;
using NavDemo;

namespace NavDemo.Services
{
    public class DbContext : Singleton<DbContext>,IDbContentService
    {
        private string DbFileName = "test1.db";
        private string DbFilePath;
        /// <summary>
        /// 获取SQL链接
        /// </summary>
        /// <returns>SQL链接</returns>
        public SQLiteConnection GetSqLiteConnection()
        {
            ISQLitePlatform platform = new SQLitePlatformWinRT();
            SQLiteConnection con = new SQLiteConnection(platform, DbFilePath);
            /*
             if (connection != null)
                return connection;
            else return connection = new SQLiteConnection(platform, DbFilePath);
             */

            

            return con;
        }
        /// <summary>
        /// 初始化Friend表
        /// </summary>
        public void initTableFriend()
        {
            string FdLocal = ApplicationData.Current.LocalFolder.Path;
            DbFilePath = Path.Combine(FdLocal, DbFileName);



            SQLiteConnection con = GetSqLiteConnection();

            int rn = con.CreateTable<Friend>(CreateFlags.None);
            con.Dispose();

            
        }
        /// <summary>
        /// 初始化Dialog表
        /// </summary>
        public void initTableDialog()
        {
            string FdLocal = ApplicationData.Current.LocalFolder.Path;
            DbFilePath = Path.Combine(FdLocal, DbFileName);



            SQLiteConnection con = GetSqLiteConnection();

            int rn = con.CreateTable<Dialog>(CreateFlags.None);
            con.Dispose();


        }

        

    }
}
