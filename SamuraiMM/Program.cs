using SamuraiMM.Model;
using SamuraiMM.Repo;

SamuraiRepo s = new();


SamuraiModel newSamurai = new();

//s.CreateSamurai();


//newSamurai.ID = 1;
//newSamurai.FirstName = "Test";
//newSamurai.LastName = "Test2";
//newSamurai.Birthdate = DateTime.Now;

//s.InsertSamurai(newSamurai);

//Console.WriteLine("Færdig");

//Console.ReadKey();

s.DeleteSamurai(1);

Console.WriteLine("Færdig");

Console.ReadKey();


