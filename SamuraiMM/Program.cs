using SamuraiMM.Model;
using SamuraiMM.Repo;

SamuraiRepo s = new();

//s.CreateSamurai();


//SamuraiModel newSamurai = new()
//{
//    ID = 1,
//    FirstName = "Test",
//    LastName = "Test2",
//    Birthdate = DateTime.Now
//};

//s.InsertSamurai(newSamurai);

//Console.WriteLine("Data indsat");

//Console.ReadKey();

//s.DeleteSamurai(1);

//Console.WriteLine("Færdig");

//Console.ReadKey();

//SamuraiModel updateSamurai = new()
//{
//    ID = 1,
//    FirstName = "Hej",
//    LastName = "Hej2",
//    Birthdate = DateTime.Now
//};

SamuraiModel updateSamurai = new()
{
    ID = 1,
    FirstName = "Hej",
    LastName = "Hej2",
    Birthdate = DateTime.Now
};



s.UpdateSamurai(updateSamurai);

//Console.WriteLine("Samurai Update");

//Console.ReadKey();