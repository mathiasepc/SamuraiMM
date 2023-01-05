using SamuraiMM.Repo;

BattlesRepo b = new();

//b.CreateTableBattles();

//BattleModel m = new BattleModel()
//{
//    EventTitle = "Battle of The Century",
//    Description = "It was the fiercest battle in the 1800",
//    EventStartDate = new DateTime(1800, 12, 31),
//    EventSlutDate = new DateTime(1820, 5, 21),
//};

//BattleModel m2 = new BattleModel()
//{
//    EventTitle = "Battle of The YEAR",
//    Description = "It was the fiercest battle in the 1925",
//    EventStartDate = new DateTime(1925, 1, 5),
//    EventSlutDate = new DateTime(1925, 6, 29),
//};


//b.InsertBattles(m);
//b.InsertBattles(m2);

//SamuraiRepo s = new();

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

//sb.CreateTableSamuraiBattles();

//SamuraiBattlesRepo s = new();

//var lam = s.ReadOneSamuraiBattles2(1);

//Console.WriteLine(lam.FirstName + " " + lam.LastName + " participated in: ");
//foreach (var item in lam.Battles)
//{
//    Console.WriteLine(item.EventTitle + " " + item.Description);
//}

//var lam = s.ReadOneBattlesSamurais2(2);

//Console.WriteLine("Title: " + lam.EventTitle + " \nParticipants: ");
//foreach (var item in lam.Samurais)
//{
//    
//}
//Console.WriteLine("Description: " + lam.Description);

//var lam = s.ReadAllBattlesAndSamurais();


//foreach (var item in lam)
//{
//    foreach (var battles in item.Battles)
//    {
//        Console.WriteLine(battles.EventTitle + " - " + battles.Description);
//    }

//    foreach (var samurais in item.Samurais)
//    {
//        Console.WriteLine(samurais.FirstName + "  " + samurais.LastName);
//    }
//}

HorseRepo h = new();
h.CreateTableHorse();

//HorseModel h1 = new()
//{
//    FirstName = "test",
//    SamuraiID = 1,
//    HorseRace = "sort"
//};

//h.InsertHorse(h1);




