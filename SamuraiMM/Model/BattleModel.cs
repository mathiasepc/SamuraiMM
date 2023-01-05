using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class BattleModel
    {
        public int ID { get; set; }
        public string EventTitle { get; set; }
        public string Description { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventSlutDate { get; set; }
        public List<SamuraiModel> Samurais { get; set; }
    }
}
