HorseRepo h = new();
SamuraiRepo s = new();
//foreach (var item in a.FilterDataBase())
//{
//    Console.WriteLine($"id: {item.ID} navn: {item.Firstname} {item.Lastname}");
//}

//s.CreateSamurai();

//h.CreateHorse();

QuoteModel horse = new();

horse = new()
{
    ID = 1,
    Firstname = "FASTRIDE",
    SamuraiID = 1
};

////h.InsertHorse(horse);

////Console.WriteLine("Hest er lavet");

////Console.ReadKey();

SamuraiModel samurai = new();

samurai = new()
{
    ID = 1,
    Firstname = "Mathias",
    Lastname = "Clausen",
};

////s.InsertSamurai(samurai);

////Console.WriteLine("Samurai oprettet");

////Console.ReadKey();

ADOHandler a = new();

//a.FilterInsertADOModel2(horse);

a.FilterInsertADOModel2(samurai);

////a.CreateDataBase();

//a.FilterInsertADOModel(null, horse, 1);

//Console.WriteLine("Hest gemt");

//Console.ReadKey();

//a.FilterInsertADOModel(samurai, null, 1);

//Console.WriteLine("Samurai indsat");

//Console.ReadKey();