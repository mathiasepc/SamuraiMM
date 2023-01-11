using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IBattleSchema
    {
        public void InsertBattleSchema(BattleSchemaModel batsam);
        public void DeleteBattleSchema(int ID);
        public void UpdateBattleSchema(BattleSchemaModel batsam, int oldSamuraiID, int oldBattlesID);
        public List<BattleSchemaModel> ReadAllBattleSchema();
        public BattleSchemaModel ReadOneBattleSchema(int SamuraiID, int BattlesID);
    }
}
