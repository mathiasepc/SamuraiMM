using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    internal class BattleModel
    {
        public int ID { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public int SamuraiId { get; set; }  
    }
}
