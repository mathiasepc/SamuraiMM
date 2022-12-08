using SamuraiMM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class SamuraiRepo : ISamurai
    {
        ADOHandler ADO = new();

        /// <summary>
        /// laver en metode der laver en samurai tabel
        /// </summary>
        public void CreateSamurai()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Samurai(Id int, FirstName nvarchar(50), LastName nvarchar(50), Birthdate datetime); ", sqlConnection);

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

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Samurai (Id, FirstName, LastName, Birthdate) values('{samurai.ID}', '{samurai.FirstName}', '{samurai.LastName}', '{samurai.Birthdate}')", sqlConnection);

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
                string sqlCommand = new($"Delete from Samurai Where ID ='{samuraiID}'");

                SqlDataAdapter sqlDataAdapter = new();

                //putter min sql commando og connectionstring i deleteCommand
                sqlDataAdapter.DeleteCommand = new(sqlCommand, sqlConnection);

                //eksekverer commandoen´og sletter rækken.
                sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
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

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE Samurai SET FirstName = '{samurai.FirstName}', LastName = '{samurai.LastName}', Birthdate = @f3 Where ID = {samurai.ID}", sqlConnection);

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

        public SamuraiModel ReadSamuraisHouse(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Samurai, Horse where Horse.SamuraiId={samuraiID} AND Samurai.Id = {samuraiID}", con);

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
                sam.horse = new HorseModel()
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Firstname = reader["Name"].ToString(),
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
                    SamuraiModel samTemp = new SamuraiModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Birthdate = reader.GetDateTime(3) };

                    //overfører den ene person til List
                    allSamurais.Add(samTemp);
                }

                //returner Listen med Data
                return allSamurais;
            }
        }

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

