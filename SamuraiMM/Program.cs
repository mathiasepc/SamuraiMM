BattlesRepo b = new();

//b.CreateTableBattles();

BattleModel m = new BattleModel()
{
    EventTitle = "Battle of The Century",
    Description = "It was the fiercest battle in the 1800",
    EventStartDate = new DateTime(1800, 12, 31),
    EventSlutDate = new DateTime(1820, 5, 21),
};

BattleModel m2 = new BattleModel()
{
    EventTitle = "Battle of The YEAR",
    Description = "It was the fiercest battle in the 1925",
    EventStartDate = new DateTime(1925, 1, 5),
    EventSlutDate = new DateTime(1925, 6, 29),
};


//b.InsertBattles(m);
//b.InsertBattles(m2);

SamuraiRepo s = new();

//s.CreateTableSamurai();

SamuraiModel m3 = new SamuraiModel()
{
    FirstName = "Hanzo",
    LastName = "Tanzo",
    Birthdate = DateTime.Now
};

SamuraiModel m4 = new SamuraiModel()
{
    FirstName = "Hikaru",
    LastName = "Shigu",
    Birthdate = DateTime.Now
};


//s.InsertSamurai(m3);
//s.InsertSamurai(m4);

SamuraiBattlesRepo sb = new();

//sb.CreateTableSamuraiBattles();

BattlesSamuariModel m5 = new BattlesSamuariModel()
{
    SamuraiID = 1,
    BattlesID = 1
};

BattlesSamuariModel m6 = new BattlesSamuariModel()
{
    SamuraiID = 2,
    BattlesID = 1
};

BattlesSamuariModel m7 = new BattlesSamuariModel()
{
    SamuraiID = 1,
    BattlesID = 2
};

//sb.InsertBattleSamurais(m5);
//sb.InsertBattleSamurais(m6);
//sb.InsertBattleSamurais(m7);

//sb.ReadAllBattlesAndSamurais();
//sb.ReadOneBattlesSamurais(1);
sb.ReadOneSamuraiBattles(1);


