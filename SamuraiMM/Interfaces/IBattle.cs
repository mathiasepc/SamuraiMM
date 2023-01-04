using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IBattle
    {
        public void CreateTableBattles();
        public void InsertBattles(BattleModel Battle);
        public void DeleteBattle(int ID);
        public void UpdateBattle(BattleModel Battle);
        public List<BattleModel> ReadAllBattles();
    }
}
