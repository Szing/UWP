using NavDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Services
{
    public class SuggestService : Singleton<SuggestService>
    {
        
        public static List<Friend> suggestItems;
        
        public void init(List<Friend> items)
        {
            suggestItems = items;
        }
        public void insert(Friend friend)
        {
            suggestItems.Add(friend);
        }
        public List<Friend> Suggest(string inputText)
        {
            return suggestItems
                .Where(i => i.nameFriend.StartsWith(inputText))
                .ToList();
        }
    }
}
