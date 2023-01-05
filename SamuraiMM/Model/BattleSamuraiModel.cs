using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class BattleSamuraiModel
    {
        public int ID { get; set; }

        public int SamuraiID { get; set; }  
        public List<SamuraiModel> Samurais { get; set; }

        public int BattlesID { get; set; }  
        public List<BattleModel> Battles { get; set; }
    }
}
