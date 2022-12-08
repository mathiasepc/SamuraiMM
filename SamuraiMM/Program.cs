SamuraiRepo s = new();


var lam = s.ReadAllSamuraiAndQuotes();


foreach (var d in lam)
{
    Console.WriteLine($"{d.FirstName} {d.LastName}");
    foreach (var lemo in d.Quotes)
    {
        Console.WriteLine(lemo.QuoteText);
    }
}