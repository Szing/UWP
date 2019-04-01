
using MVVMSidekick.ViewModels;
using SQLite.Net.Attributes;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace NavDemo

{

    [Table("per_info")]

    public class Friend
    {

        

        [Column("id")]

        [PrimaryKey]

        [AutoIncrement()]

        public int idFriend { get; set; }



        [Column("name")]

        [NotNull]

        public string nameFriend { get; set; }



        [Column("icon")]

        public string iconFriend { get; set; }



        [Column("nickname")]

        [NotNull]

        public string nickNameFriend { get; set; }

    }

}
