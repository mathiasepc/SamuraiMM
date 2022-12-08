ADOHandler a = new();

HorseModel horse = new();

horse = new()
{
    ID = 1,
    Firstname = "FASTRIDE",
    SamuraiID = 1
};


SamuraiModel samurai = new();

samurai = new()
{
    ID = 1,
    FirstName = "Mathias",
    LastName = "Clausen"
};

//a.CreateDataBase();

//Console.WriteLine("Database lavet");

//Console.ReadKey();

a.InsertIntoADOModel(null, horse);

Console.WriteLine("Hest gemt");

Console.ReadKey();

a.InsertIntoADOModel(samurai, null);

Console.WriteLine("Samurai indsat");

Console.ReadKey();