using SamuraiMM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class BattlesRepo : IBattle
    {
        ADOHandler ADO = new();

        public void CreateTableBattles()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Battle(ID int Identity(1,1) Primary Key, EventTitle nvarchar(50), Description nvarchar(200), EventStartDate datetime, EventSlutDate datetime, Removed int); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="quote"></param>
        public void InsertBattles(BattleModel Battle)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Battle (EventTitle, Description, EventStartDate, EventSlutDate, Removed) values('{Battle.EventTitle}', '{Battle.Description}',@f3,@f4, '1')", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@f3", Battle.EventStartDate);
                sqlCommand.Parameters.AddWithValue("@f4", Battle.EventSlutDate);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// vores metode for at slette
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteBattle(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                SqlCommand sqlCommand = new($"UPDATE Battle SET Removed = 2 Where ID = {ID}", sqlConnection);

                SqlDataAdapter sqlDataAdapter = new();

                //eksekverer commandoen´og sletter rækken.
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vi laver en metode som skal opdatere databasen
        /// </summary>
        /// <param name="samurai"></param>
        public void UpdateBattle(BattleModel Battle)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE Battle SET EventTitle = '{Battle.EventTitle}', Description = '{Battle.Description}',EventStartDate = @f3, EventSlutDate = @f4 Where ID = {Battle.ID}", sqlConnection);

                //Da database ikke kan forstå datetime, parse vi den ind i en variable for sig.
                commandChange.Parameters.AddWithValue("@f3", Battle.EventStartDate);
                commandChange.Parameters.AddWithValue("@f4", Battle.EventSlutDate);

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// henter en blade med samurai i
        /// </summary>
        /// <param name="battleID"></param>
        /// <returns></returns>
        public BattleModel ReadOneBattle(int battleID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Battle where id={battleID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                BattleModel bat = new BattleModel();

                //de forskellige værdier fra databasen
                bat.ID = Convert.ToInt32(reader["id"]);
                bat.EventTitle = reader["EventTitle"].ToString();
                bat.Description = reader["Description"].ToString();
                bat.EventStartDate = Convert.ToDateTime(reader["EventStartDate"]);
                bat.EventSlutDate = Convert.ToDateTime(reader["EventSlutDate"]);

                //returner den nye model
                return bat;
            }
        }

        /// <summary>
        /// henter alle battles
        /// </summary>
        /// <returns></returns>
        public List<BattleModel> ReadAllBattles()
        {
            //vi laver en list som vi indsætter data'en i
            List<BattleModel> allBattles = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Battle", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    BattleModel BattleTemp = new BattleModel() { ID = reader.GetInt32(0), EventTitle = reader.GetString(1), Description = reader.GetString(2), EventStartDate = reader.GetDateTime(3), EventSlutDate = reader.GetDateTime(4), Removed = reader.GetInt32(5)};

                    //overfører den ene person til List
                    allBattles.Add(BattleTemp);
                }
                //returner Listen med Data
                return allBattles;
            }
        }

        /// <summary>
        /// henter alle battle som ikke er slettet
        /// </summary>
        /// <returns></returns>
        public List<BattleModel> ReadAllAliveBattles()
        {
            //vi laver en list som vi indsætter data'en i
            List<BattleModel> allBattles = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Battle where Removed = 1", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    BattleModel BattleTemp = new BattleModel() { ID = reader.GetInt32(0), EventTitle = reader.GetString(1), Description = reader.GetString(2), EventStartDate = reader.GetDateTime(3), EventSlutDate = reader.GetDateTime(4), Removed = reader.GetInt32(5) };

                    //overfører den ene person til List
                    allBattles.Add(BattleTemp);
                }
                //returner Listen med Data
                return allBattles;
            }
        }
    }
}
