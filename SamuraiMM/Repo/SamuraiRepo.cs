using SamuraiMM.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SamuraiMM.Repo
{
    public class SamuraiRepo : ISamurai
    {
        ADOHandler ADO = new();

        /// <summary>
        /// laver en metode der laver en samurai tabel
        /// </summary>
        public void CreateTableSamurai()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Samurai(ID int Identity(1,1) Primary Key, FirstName nvarchar(50), LastName nvarchar(50), Birthdate datetime, Deleted int, ClanID int Foreign KEY references Clan(ID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="samurai"></param>
        public void InsertSamurai(SamuraiModel samurai)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen 
                SqlCommand sqlCommand = new();

                //Indsætter i databasen
                if (samurai.ClanID != 0)
                {
                    sqlCommand = new($"INSERT INTO Samurai (FirstName, LastName, Birthdate, Deleted, ClanID) values('{samurai.FirstName}', '{samurai.LastName}', @f3, '1', {samurai.ClanID}) ", sqlConnection);
                }
                else
                {
                    sqlCommand = new($"INSERT INTO Samurai (FirstName, LastName, Birthdate, Deleted) values('{samurai.FirstName}', '{samurai.LastName}', @f3, '1')", sqlConnection);
                }

                //Da database ikke kan forstå datetime, parse vi den ind i en variable for sig.
                sqlCommand.Parameters.AddWithValue("@f3", samurai.Birthdate);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode som kan slette i samurai tabellen.
        /// </summary>
        /// <param name="samuraiID"></param>
        public void DeleteSamurai(int samuraiID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                SqlCommand sqlCommand = new($"UPDATE Samurai SET Deleted = 2 Where ID = {samuraiID}", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vi laver en metode som skal opdatere databasen
        /// </summary>
        /// <param name="samurai"></param>
        public void UpdateSamurai(SamuraiModel samurai)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen
                SqlCommand commandChange = new();

                //Opdater i databasen
                if (samurai.ClanID != 0)
                {
                    commandChange = new($"UPDATE Samurai SET FirstName = '{samurai.FirstName}', LastName = '{samurai.LastName}', Birthdate = @f3 , ClanID = '{samurai.ClanID}' Where ID = {samurai.ID}", sqlConnection);
                }
                else
                {
                    //Vi har hardcoded ClanID = NULL her, hvis man gerne vil sætte samurairen til at være clanless
                    commandChange = new($"UPDATE Samurai SET FirstName = '{samurai.FirstName}', LastName = '{samurai.LastName}', Birthdate = @f3, ClanID = NULL Where ID = {samurai.ID}", sqlConnection);
                }

                //Da database ikke kan forstå datetime, parse vi den ind i en variable for sig.
                commandChange.Parameters.AddWithValue("@f3", samurai.Birthdate);

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Denne metode læser kun en samurai fra databasen
        /// </summary>
        /// <param name="samuraiID"></param>
        /// <returns></returns>
        public SamuraiModel ReadOneSamurai(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando

                SqlCommand cmd = new SqlCommand($"select * from Samurai where id={samuraiID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                //de forskellige værdier fra databasen
                sam.ID = Convert.ToInt32(reader["id"]);
                sam.FirstName = reader["FirstName"].ToString();
                sam.LastName = reader["LastName"].ToString();
                sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);

                //returner den nye model
                return sam;
            }
        }

        /// <summary>
        /// Denne metode læser kun en samurai fra databasen
        /// </summary>
        /// <param name="samuraiID"></param>
        /// <returns></returns>
        public SamuraiModel ReadOneAliveSamurai(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando

                SqlCommand cmd = new SqlCommand($"select * from Samurai where id={samuraiID} AND deleted = 1", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                //de forskellige værdier fra databasen
                sam.ID = Convert.ToInt32(reader["id"]);
                sam.FirstName = reader["FirstName"].ToString();
                sam.LastName = reader["LastName"].ToString();
                sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);

                //returner den nye model
                return sam;
            }
        }

        public SamuraiModel ReadAliveSamuraisHouse(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Samurai, Horse where Horse.SamuraiId={samuraiID} AND Samurai.Id = {samuraiID} AND Samurai.Deleted = 1", con);

                con.Open();

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                //de forskellige værdier fra databasen
                sam.ID = Convert.ToInt32(reader["id"]);
                sam.FirstName = reader["FirstName"].ToString();
                sam.LastName = reader["LastName"].ToString();
                sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);
                sam.Horse = new HorseModel()
                {
                    Name = reader["Name"].ToString(),
                    HorseRace = reader["HorseRace"].ToString(),
                    SamuraiID = Convert.ToInt32(reader["SamuraiId"])
                };

                //returner den nye model
                return sam;
            }
        }

        /// <summary>
        /// Den læser alle samurai's fra databasen
        /// </summary>
        /// <returns></returns>
        public List<SamuraiModel> ReadAllSamurais()
        {
            //vi laver en list som vi indsætter data'en i
            List<SamuraiModel> allSamurais = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Samurai", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    SamuraiModel samTemp = new SamuraiModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Birthdate = reader.GetDateTime(3), Deleted = reader.GetInt32(4) };

                    //overfører den ene person til List
                    allSamurais.Add(samTemp);
                }
                //returner Listen med Data
                return allSamurais;
            }
        }

        public List<SamuraiModel> ReadAllAliveSamurais()
        {
            //vi laver en list som vi indsætter data'en i
            List<SamuraiModel> allSamurais = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Samurai where Samurai.Deleted != 2", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    SamuraiModel samTemp = new SamuraiModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Birthdate = reader.GetDateTime(3), Deleted = reader.GetInt32(4) };

                    //overfører den ene person til List
                    allSamurais.Add(samTemp);
                }
                //returner Listen med Data
                return allSamurais;
            }
        }

        //Vi bruger den ikke men det til visning
        //
        //
        //


        public SamuraiModel ReadSamuraisQuotes(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Samurai, Quotes where Quotes.SamuraiId={samuraiID} AND Samurai.Id = {samuraiID}", con);

                con.Open();

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                //vi laver en list som vi kan putte værdierne ind i
                sam.Quotes = new List<QuoteModel>();
                while (reader.Read())
                {
                    //de forskellige værdier fra databasen
                    sam.ID = Convert.ToInt32(reader["id"]);
                    sam.FirstName = reader["FirstName"].ToString();
                    sam.LastName = reader["LastName"].ToString();
                    sam.Quotes.Add(new QuoteModel() { QuoteText = reader["QuoteText"].ToString() });
                }
                return sam;
            }
        }

        public SamuraiModel ReadOneSamuraisProps(int tempsamuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand("SELECT Samurai.FirstName AS Name, Samurai.LastName AS Lastname, Samurai.Birthdate AS BirthDate, Quote.QuoteText AS QuoteText, Horse.Name AS HorseName, Clan.ClanName as ClanName, Blade.Name as BladeName, Battle.EventTitle as EventTitle, Samurai.ID as SamuraiID, Samurai.Deleted as Deleted" +
                    " FROM Samurai " +
                    "INNER JOIN Quote ON Samurai.ID=Quote.SamuraiID " +
                    "INNER JOIN Horse ON Samurai.ID=Horse.SamuraiID " +
                    "INNER JOIN Clan on Clan.ID=Samurai.ClanID " +
                    "INNER JOIN Blade on Blade.SamuraiID=Samurai.ID " +
                    "JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID " +
                    "JOIN Battle ON BattleSchema.BattlesID = Battle.ID " +
                   $"WHERE Samurai.ID={tempsamuraiID}", con);

                con.Open();

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                sam.Quotes = new();
                sam.Blades = new();
                sam.Battles = new();
                while (reader.Read())
                {
                    //de forskellige værdier fra databasen
                    sam.ID = Convert.ToInt32(reader["SamuraiID"]);
                    sam.FirstName = reader["Name"].ToString();
                    sam.LastName = reader["Lastname"].ToString();
                    sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);
                    sam.Deleted = Convert.ToInt32(reader["Deleted"]);
                    sam.Quotes.Add(new QuoteModel()
                    {
                        QuoteText = reader["QuoteText"].ToString(),
                        //SamuraiID = Convert.ToInt32(reader["SamuraiId"]) ? 5: 0
                        SamuraiID = reader["samuraiId"] != null ? Convert.ToInt32(reader["SamuraiId"]) : 0
                    });
                    sam.Horse = new HorseModel()
                    {
                        Name = reader["HorseName"].ToString()
                    };
                    sam.Clan = new ClanModel()
                    {
                        ClanName = reader["ClanName"].ToString()
                    };
                    sam.Blades.Add(new BladeModel()
                    {
                        Name = reader["BladeName"].ToString()
                    });
                    sam.Battles.Add(new BattleModel()
                    {
                        EventTitle = reader["EventTitle"].ToString()
                    });

                }

                //?: operator - the ternary conditional operator (Conditional If statement)
                //if (reader["SamuraiId"] != null) //Hvis samurai har en horse så gør det her
                //{
                //    samuraiID = Convert.ToInt32(reader["SamuraiId"]); // <-
                //}
                //else
                //{
                //    samuraiID = 0;
                //}

                //returner den nye model
                return sam;
            }
        }

        public List<SamuraiModel> ReadAllSamuraiAndQuotes()
        {
            //vi laver en list som vi indsætter data'en i
            List<SamuraiModel> allSamurais = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Samurai INNER JOIN Quotes ON Quotes.SamuraiId = Samurai.Id", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    SamuraiModel samTemp = new SamuraiModel();
                    samTemp.Quotes = new List<QuoteModel>();

                    samTemp.ID = Convert.ToInt32(reader["id"]);
                    samTemp.FirstName = reader["FirstName"].ToString();
                    samTemp.LastName = reader["LastName"].ToString();
                    samTemp.Quotes.Add(new QuoteModel() { QuoteText = reader["QuoteText"].ToString() });
                    //overfører den ene person til List
                    allSamurais.Add(samTemp);
                }
                //returner Listen med Data
                return allSamurais;
            }
        }
    }
}

