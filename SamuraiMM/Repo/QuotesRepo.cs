using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class QuotesRepo
    {
        ADOHandler ADO = new();
        public void CreateQuotes()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                using SqlCommand command = new SqlCommand($"CREATE TABLE Quotes(Id int, QuoteText nvarchar(50), SamuraiId int); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        public void InsertQuotes(QuoteModel quotes)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Quotes(Id, QuoteText, SamuraiId) values('{quotes.ID}', '{quotes.QuoteText}', '{quotes.SamuraiId}')");

                //tilføjer min ConnectionString til sqlCommand object
                sqlCommand.Connection = sqlConnection;

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

    }
}
