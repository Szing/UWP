using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public interface ISuggestService
    {

        /// <summary>
        /// 初始化建议项目
        /// </summary>
        /// <param name="items">已经获得的Friend列表</param>
        void init(List<Friend> items);

        /// <summary>
        /// 加入新的建议项
        /// </summary>
        /// <param name="friend">需要新加入的用户</param>
        void insert(Friend friend);

        /// <summary>
        /// 根据搜索框的首字母返回建议项
        /// </summary>
        /// <param name="inputText">建议框的输入</param>
        /// <returns>建议列表</returns>
        List<Friend> Suggest(string inputText);
        
    }
}
