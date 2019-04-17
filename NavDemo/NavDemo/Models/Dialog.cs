
using MVVMSidekick.ViewModels;
using SQLite.Net.Attributes;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace NavDemo

{
    
    [Table("tableDialog")]

    public class Dialog
    {



        [Column("idDialog")]

        [PrimaryKey]

        [AutoIncrement()]

        public int idDialog { get; set; }



        [Column("idFriend")]

        [NotNull]

        public int idFriend { get; set; }

        [Column("nameFriend")]
       
        public string nameFriend { get; set; }

        [Column("nickNameFriend")]
        
        public string nickNameFriend { get; set; }

        [Column("imageDialog")]

        public string imageDialog { get; set; }

        [Column("describeDialog")]

        public string describeDialog { get; set; }


        [Column("textDialog")]

        public string textDialog { get; set; }

        [Column("timeDialog")]
        public string timeDialog { get; set; }

        [Column("flagTime")]
        public int flagTime { get; set; }

        public void Set(int idD,int idF,string name,string nickname,
            string img,string des,string text,string time,int flag)
        {
            idDialog = idD;
            idFriend = idF;
            nameFriend = name;
            nickNameFriend = nickname;
            describeDialog = des;
            textDialog = text;
            timeDialog = time;
            flagTime = flag;
        }
    }

}
