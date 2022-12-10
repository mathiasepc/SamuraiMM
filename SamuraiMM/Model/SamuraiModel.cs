using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    internal class SamuraiModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public HorseModel Horse { get; set; }
        public List<QuoteModel> Quotes { get; set; }
        public ClanModel Clan { get; set; } 
        public List<BattleModel> Battles { get; set; }  
    }
}
