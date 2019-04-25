using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public interface IDbContentService
    {
        /// <summary>
        /// 获取SQL链接
        /// </summary>
        /// <returns>SQL链接</returns>
        SQLite.Net.SQLiteConnection GetSqLiteConnection();
        /// <summary>
        /// 初始化Friend表
        /// </summary>
        void initTableFriend();
        /// <summary>
        /// 初始化Dialog表
        /// </summary>
        void initTableDialog();
    }
}
