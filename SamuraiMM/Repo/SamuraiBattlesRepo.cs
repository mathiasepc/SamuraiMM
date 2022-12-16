using SamuraiMM.Interfaces;
using SamuraiMM.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class SamuraiBattlesRepo
    { 

        ADOHandler ADO = new();

        public void CreateTableSamuraiBattles()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE BattleSchema(ID int Identity(1,1) Primary Key, SamuraiID int Foreign KEY references Samurai(ID), BattlesID int Foreign KEY references Battle(ID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        public void InsertBattleSamurais(BattlesSamuariModel batsam)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO BattleSchema (SamuraiID, BattlesID) values('{batsam.SamuraiID}', '{batsam.BattlesID}')", sqlConnection);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void ReadOneBattlesSamurais(int battlesID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                string query = "SELECT Samurai.FirstName + ' ' + Samurai.LastName as SamuraiName, Battle.EventTitle as Title, Battle.Description as Description, Battle.EventStartDate as StartDate, Battle.EventSlutDate as EndDate " +
               $"FROM Samurai " +
               $"JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID " +
               $"JOIN Battle ON BattleSchema.BattlesID = Battle.ID " +
               $"WHERE BattlesID={battlesID}";

                using (SqlConnection connection = new SqlConnection(ADO.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["SamuraiName"]} participated in {reader["Title"]}. \nEvent Description: {reader["Description"]} \nStart: {reader["StartDate"]} End: {reader["EndDate"]} \n");
                    }
                    reader.Close();
                }
            }
        }

        public void ReadOneSamuraiBattles(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                string query = "SELECT Samurai.FirstName + ' ' + Samurai.LastName as SamuraiName, Battle.EventTitle as Title, Battle.Description as Description, Battle.EventStartDate as StartDate, Battle.EventSlutDate as EndDate " +
               $"FROM Samurai " +
               $"JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID " +
               $"JOIN Battle ON BattleSchema.BattlesID = Battle.ID " +
               $"WHERE Samurai.ID={samuraiID}";

                using (SqlConnection connection = new SqlConnection(ADO.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["SamuraiName"]} participated in {reader["Title"]}. \nEvent Description: {reader["Description"]} \nStart: {reader["StartDate"]} End: {reader["EndDate"]} \n");
                    }
                    reader.Close();
                }
            }
        }

        public void ReadAllBattlesAndSamurais()
        {
            string query = "SELECT Samurai.FirstName + ' ' + Samurai.LastName as SamuraiName, Battle.EventTitle as Title, Battle.Description as Description, Battle.EventStartDate as StartDate, Battle.EventSlutDate as EndDate " +
               "FROM Samurai " +
               "JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID " +
               "JOIN Battle ON BattleSchema.BattlesID = Battle.ID";

            using (SqlConnection connection = new SqlConnection(ADO.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["SamuraiName"]} participated in {reader["Title"]}. \nEvent Description: {reader["Description"]} \nStart: {reader["StartDate"]} End: {reader["EndDate"]} \n");
                }
                reader.Close();
            }
        }

    }
}
