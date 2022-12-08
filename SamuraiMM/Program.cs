SamuraiRepo s = new();


var sam = s.ReadSamuraisHouse(1);

Console.WriteLine($"Samurai Navn: {sam.FirstName} {sam.LastName} Hest navn: {sam.horse.Name}");  