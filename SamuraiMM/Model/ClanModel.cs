using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class ClanModel
    {
        public int ID { get; set; }
        public string ClanName { get; set; }
        public int SamuraiID { get; set; }
        public SamuraiModel ClanLeder { get; set; }
        public List<SamuraiModel> Samurais {get; set; }
    }
}
