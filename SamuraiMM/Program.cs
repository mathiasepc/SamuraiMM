ADOHandler a = new();

//HorseModel horse = new();

//horse = new()
//{
//    ID = 1,
//    Firstname = "FASTRIDE",
//    SamuraiID = 1
//};


//SamuraiModel samurai = new();

//samurai = new()
//{
//    ID = 1,
//    FirstName = "Mathias",
//    LastName = "Clausen"
//};
foreach(var item in a.CheckDataBase())
{
    Console.WriteLine($"id: {item.ID} navn: {item.Firstname} {item.Lastname}");
    Console.ReadKey();
}


foreach (var item in a.CheckDataBase())
{
    Console.WriteLine($"id: {item.ID} navn: {item.Firstname} {item.Lastname}");
    Console.ReadKey();
}

//a.FilterInsertADOModel(null, horse);

//Console.WriteLine("Hest gemt");

//Console.ReadKey();

//a.FilterInsertADOModel(samurai, null);

//Console.WriteLine("Samurai indsat");

//Console.ReadKey();