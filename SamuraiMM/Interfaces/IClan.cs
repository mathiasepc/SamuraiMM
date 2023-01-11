using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IClan
    {
        public void InsertClan(ClanModel clan);
        public void DeleteClan(int ID);
        public void UpdateClan(ClanModel clan);
        public ClanModel ReadOneClan(int clanID);
        public List<ClanModel> ReadAllClansAndSamurais();
        public List<ClanModel> ReadAllClansExcludingSamurais();
        public List<ClanModel> ReadAllAliveClan();
    }
}
