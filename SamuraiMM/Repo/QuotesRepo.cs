using SamuraiMM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class QuotesRepo : IQuotes
    {
        ADOHandler ADO = new();

        public void CreateTableQuote()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();


                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Quote(ID int Identity(1,1) Primary Key, QuoteText nvarchar(50), SamuraiID int Foreign KEY references Samurai(ID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="quote"></param>
        public void InsertQuote(QuoteModel quote)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand sqlCommand = new();

                //henter samurai repo
                SamuraiRepo sam = new SamuraiRepo();

                //henter døde samurai
                var aliveSamurai = sam.ReadAllAliveSamurais();

                foreach (var alive in aliveSamurai)
                {
                    //hvis indtastede er forskellige for død samurai
                    if (quote.SamuraiID == alive.ID)
                    {
                        //istansiere SqlCommand klassen og indsætter i databasen
                        sqlCommand = new($"INSERT INTO Quote (QuoteText, SamuraiID) values('{quote.QuoteText}', '{quote.SamuraiID}')", sqlConnection);
                    }
                }
                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode for at slette
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteQuote(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from Quote Where ID ='{ID}'");

                SqlDataAdapter sqlDataAdapter = new();

                //putter min sql commando og connectionstring i deleteCommand
                sqlDataAdapter.DeleteCommand = new(sqlCommand, sqlConnection);

                //eksekverer commandoen´og sletter rækken.
                sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode som skal opdatere databasen
        /// </summary>
        /// <param name="samurai"></param>
        public void UpdateQuote(QuoteModel quote, int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand commandChange = new();

                //henter samurai repo
                SamuraiRepo samurai = new();

                //henter døde samurai
                var aliveSamurai = samurai.ReadAllAliveSamurais();

                foreach (var alive in aliveSamurai)
                {
                    //hvis id er forskellige for den døde
                    if (quote.SamuraiID == alive.ID)
                    {
                        //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                        commandChange = new($"UPDATE Quote SET QuoteText = '{quote.QuoteText}', SamuraiId = {quote.SamuraiID} Where ID = {ID}", sqlConnection);
                    }
                }

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en som henter et quote
        /// </summary>
        /// <param name="quoteID"></param>
        /// <returns></returns>
        public QuoteModel ReadOneQuote(int quoteID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Quote where id={quoteID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                QuoteModel quo = new QuoteModel();

                //de forskellige værdier fra databasen
                quo.QuoteText = reader["QuoteText"].ToString();
                quo.SamuraiID = Convert.ToInt32(reader["SamuraiID"]);

                //returner den nye model
                return quo;
            }
        }

        /// <summary>
        /// laver en metode som henter quotes med samurai
        /// </summary>
        /// <returns></returns>
        public List<QuoteModel> ReadAllQuotesWithSamuraiName()
        {
            //vi laver en list som vi indsætter data'en i
            List<QuoteModel> allQuotes = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando der henter quotes samt navne fra samurai
                SqlCommand command = new SqlCommand("SELECT * FROM Quote Join Samurai on Quote.SamuraiID=Samurai.ID", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    QuoteModel quoteTemp = new QuoteModel()
                    {
                        ID = Convert.ToInt32(reader.GetInt32(0)),
                        QuoteText = reader["QuoteText"].ToString(),
                        SamuraiID = Convert.ToInt32(reader["SamuraiID"]),
                        Samurai = new SamuraiModel
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Deleted = Convert.ToInt32(reader["Deleted"])
                        }
                    };

                    //overfører den ene person til List
                    allQuotes.Add(quoteTemp);
                }
                //returner Listen med Data
                return allQuotes;
            }
        }
    }
}
