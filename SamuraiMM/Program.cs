using SamuraiMM.Repo;

BattlesRepo b = new();

//b.CreateTableBattles();

//BattleModel m = new BattleModel()
//{
//    EventTitle = "Battle of The Century",
//    Description = "It was the fiercest battle in the 18g00",
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

SamuraiRepo samd = new();

//samd.InsertSamurai(m3);
//samd.InsertSamurai(m4);

BattleSchemaRepo sb = new();

//sb.CreateTableSamuraiBattles();

BattleSchemaModel m5 = new BattleSchemaModel()
{
    SamuraiID = 1,
    BattlesID = 1
};

BattleSchemaModel m6 = new BattleSchemaModel()
{
    SamuraiID = 2,
    BattlesID = 1
};

BattleSchemaModel m7 = new BattleSchemaModel()
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


//HorseModel h1 = new()
//{
//    FirstName = "test",
//    SamuraiID = 1,
//    HorseRace = "sort"
//};

//h.InsertHorse(h1);

ClanRepo cl = new();
SamuraiRepo sam = new();
HorseRepo hor = new();
QuotesRepo q = new();
BladeRepo blade = new();
BattleSchemaRepo s = new();

cl.CreateTableClan();
Console.ReadKey();
sam.CreateTableSamurai();
Console.ReadKey();
hor.CreateTableHorse();
q.CreateTableQuote();
blade.CreateTableBlade();
b.CreateTableBattles();
Console.ReadKey();
s.CreateTableBattleSchema();
Console.ReadKey();






Login l = new();

//l.CreateTableLogin();

while (true)
{
    Console.Clear();
    Console.WriteLine("Vælg følgende:");
    Console.WriteLine("1 = opret");
    Console.WriteLine("2 = Login");
    Console.WriteLine("3 = logud");

    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.D1:
            Console.Clear();
            bool checkRegex = true;
            string emailRegistrer = string.Empty;

            while (checkRegex = false) ;
            {
                Console.WriteLine("Registrer email?");
                emailRegistrer = Console.ReadLine();
                checkRegex = l.ValidateEmail(emailRegistrer);
            }

            Console.WriteLine("Registrer password?");

            string passwordRegistrer = Console.ReadLine();

            l.CreateLogin(emailRegistrer, passwordRegistrer);

            Console.WriteLine("Oprettet");

            Console.ReadKey();
            break;
        case ConsoleKey.D2:
            Console.Clear();
            Console.WriteLine("Indtast din Email");

            string email = Console.ReadLine();

            Console.WriteLine("Indtast dit password");

            string password = Console.ReadLine();

            bool answer = l.GetUser(email, password);

            if (answer = true)
            {
                Console.WriteLine("Du inde");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("forkert");
                Console.ReadKey();
            }
            break;
        case ConsoleKey.D3:
            Console.Clear();
            string logOutAnswer = l.UserLogOut();
            Console.WriteLine($"{logOutAnswer}");
            Console.ReadKey();
            break;
        default:
            Console.Clear();
            Console.WriteLine("Noget gik galt");
            Console.ReadKey();
            break;
    }
}






