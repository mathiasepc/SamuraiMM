using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    internal class ADOModel
    {
        //Med denne opstilling kan vi både søge på en rækkes specifikke id eller et bestemt ID ift. Samurai eller hest
        public int ID { get; set; }
        public int SamuraiID { get; set; }
        public int HorseID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //hestens samuraiID
        public int HorsesSamuraiID { get; set; }

    }
}
