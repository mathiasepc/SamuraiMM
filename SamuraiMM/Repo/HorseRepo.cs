using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class HorseRepo
    {
        ADOHandler ADO = new();

        public void CreateHorse()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                using SqlCommand command = new SqlCommand($"CREATE TABLE Horse(ID int, Name nvarchar(50), SamuraiId int); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        public void Create(SqlCommand cmd)
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                using SqlCommand command = cmd;

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="horse"></param>
        public void InsertHorse(HorseModel horse)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Horse (Id, Name, SamuraiId) values('{horse.ID}', '{horse.Name}', '{horse.SamuraiId}')");

                //tilføjer min ConnectionString til sqlCommand object
                sqlCommand.Connection = sqlConnection;

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
