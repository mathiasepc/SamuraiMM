using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IBattle
    {
        public void InsertBattles(BattleModel Battle);
        public void DeleteBattle(int ID);
        public void UpdateBattle(BattleModel Battle);
        public List<BattleModel> ReadAllBattles();
        public BattleModel ReadOneBattle(int battleID);
        public List<BattleModel> ReadAllAliveBattles();
    }
}
