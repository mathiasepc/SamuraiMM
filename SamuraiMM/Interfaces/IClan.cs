using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    internal interface IClan
    {
        public void CreateClan();
        public void InsertClan(ClanModel clan);
        public void DeleteClan(int ID);
        public void UpdateClan(ClanModel clan);
        public List<ClanModel> ReadAllClans();
    }
}
