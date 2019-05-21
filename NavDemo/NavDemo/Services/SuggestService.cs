using NavDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public class SuggestService : Singleton<SuggestService>,ISuggestService
    {
        
        //单例的建议项
        public static List<Friend> suggestItems;

        /// <summary>
        /// 初始化建议项目
        /// </summary>
        /// <param name="items">已经获得的Friend列表</param>
        public void init(List<Friend> items)
        {
            suggestItems = items;
        }
            
        /// <summary>
        /// 加入新的建议项
        /// </summary>
        /// <param name="friend">需要新加入的用户</param>
        public void insert(Friend friend)
        {
            suggestItems.Add(friend);
        }

        /// <summary>
        /// 根据搜索框的首字母返回建议项
        /// </summary>
        /// <param name="inputText">建议框的输入</param>
        /// <returns>建议列表</returns>
        public List<Friend> Suggest(string inputText)
        {
            return suggestItems
                .Where(i => i.nameFriend.StartsWith(inputText))
                .ToList();
        }
    }
}
