﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IBattleSchema
    {
        public void CreateTableBattleSchema();
        public void InsertBattleSchema(BattleSchemaModel batsam);
        public void DeleteBattleSchema(int ID);
        public void UpdateBattleSchema(BattleSchemaModel batsam);
        public List<BattleSchemaModel> ReadAllBattleSchema();
        public SamuraiModel ReadOneSamuraiBattles2(int samuraiID);
    }
}