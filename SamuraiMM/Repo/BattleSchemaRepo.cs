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
    public class BattleSchemaRepo : IBattleSchema
    { 

        ADOHandler ADO = new();

        public void CreateTableBattleSchema()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE BattleSchema(SamuraiID int Foreign KEY references Samurai(ID), BattlesID int Foreign KEY references Battle(ID), PRIMARY KEY(SamuraiID, BattlesID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        public void InsertBattleSchema(BattleSchemaModel batsam)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
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

        public void DeleteBattleSchema(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from BattleSchema Where SamuraiID ='{ID}'");

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
        public void UpdateBattleSchema(BattleSchemaModel batsam, int oldSamuraiID, int oldBattlesID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE BattleSchema SET BattlesID = '{batsam.BattlesID}', SamuraiID = '{batsam.SamuraiID}' Where SamuraiID = {oldSamuraiID} And BattlesID = {oldBattlesID}", sqlConnection);

                //eksekver
                commandChange.ExecuteNonQuery();
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

                    reader.Read();
                    Console.WriteLine($"{reader["SamuraiName"]} participated in: ");

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["SamuraiName"]} participated in: ");
                        Console.Write($"{reader["Title"]}. \nEvent Description: {reader["Description"]} \nStart: {reader["StartDate"]} End: {reader["EndDate"]} \n");
                    }
                    reader.Close();
                }
            }
        }

        public SamuraiModel ReadOneSamuraiBattles2(int samuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Samurai JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID JOIN Battle ON BattleSchema.BattlesID = Battle.ID WHERE Samurai.ID={samuraiID}", con);

                con.Open();

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //vi laver en nu model hvor vi indsætter værdierne
                SamuraiModel sam = new SamuraiModel();

                //vi laver en list som vi kan putte værdierne ind i
                sam.Battles = new List<BattleModel>();
                while (reader.Read())
                {
                    //de forskellige værdier fra databasen
                    sam.ID = Convert.ToInt32(reader["id"]);
                    sam.FirstName = reader["FirstName"].ToString();
                    sam.LastName = reader["LastName"].ToString();
                    sam.Battles.Add(new BattleModel() { EventTitle = reader["EventTitle"].ToString() });
                }
                return sam;
            }
        }

        //public BattleModel ReadOneBattlesSamurais2(int battlesID)
        //{
        //    using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
        //    {
        //        //laver en sql commando
        //        SqlCommand cmd = new SqlCommand($"select * from Samurai JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID JOIN Battle ON BattleSchema.BattlesID = Battle.ID WHERE Battle.ID={battlesID}", con);

        //        con.Open();

        //        //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        //vi laver en nu model hvor vi indsætter værdierne
        //        BattleModel bat = new BattleModel();

        //        //vi laver en list som vi kan putte værdierne ind i
        //        bat.Samurais = new List<SamuraiModel>();
        //        while (reader.Read())
        //        {
        //            //de forskellige værdier fra databasen
        //            bat.ID = Convert.ToInt32(reader["id"]);
        //            bat.EventTitle = reader["EventTitle"].ToString();
        //            bat.Description = reader["Description"].ToString();
        //            bat.Samurais.Add(new SamuraiModel() { FirstName = reader["FirstName"].ToString(), LastName = reader["LastName"].ToString() });
        //        }
        //        return bat;
        //    }
        //}

        public void ReadAllBattlesAndSamurais1()
        {
            //laver en sql commando
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

        public BattleSchemaModel ReadOneBattleSchema(int tempSamuraiID, int tempBattlesID)
        {
            //vi laver en list som vi indsætter data'en i

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand($"SELECT * FROM Samurai JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID JOIN Battle ON BattleSchema.BattlesID = Battle.ID where (SamuraiID = {tempSamuraiID}) and (BattlesID = {tempBattlesID})", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                BattleSchemaModel batsamTemp = new BattleSchemaModel();
                batsamTemp.Battles = new List<BattleModel>();
                batsamTemp.BattlesID = Convert.ToInt32(reader["BattlesID"]);
                batsamTemp.Samurais = new List<SamuraiModel>();
                batsamTemp.SamuraiID = Convert.ToInt32(reader["SamuraiID"]);

                //laver et while loop for at få alt data fra databasen
      
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    batsamTemp.Battles.Add(new BattleModel()
                    {
                        EventTitle = reader["EventTitle"].ToString(),
                        Description = reader["Description"].ToString(),
                        EventStartDate = Convert.ToDateTime(reader["EventStartDate"]),
                        EventSlutDate = Convert.ToDateTime(reader["EventSlutDate"]),
                    });

                    batsamTemp.Samurais.Add(new SamuraiModel()
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Birthdate = Convert.ToDateTime(reader["Birthdate"]),
                    });
               
                return batsamTemp;
            }
        }

        public List<BattleSchemaModel> ReadAllBattleSchema()
        {
            //vi laver en list som vi indsætter data'en i
            List<BattleSchemaModel> AllBatSams = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Samurai JOIN BattleSchema ON Samurai.ID = BattleSchema.SamuraiID JOIN Battle ON BattleSchema.BattlesID = Battle.ID", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    BattleSchemaModel batsamTemp = new BattleSchemaModel();

                    batsamTemp.Battles = new List<BattleModel>();
                    batsamTemp.BattlesID = Convert.ToInt32(reader["BattlesID"]);
                    batsamTemp.Samurais = new List<SamuraiModel>();
                    batsamTemp.SamuraiID = Convert.ToInt32(reader["SamuraiID"]);

                    batsamTemp.Battles.Add(new BattleModel() { 
                        EventTitle = reader["EventTitle"].ToString(),
                        Description = reader["Description"].ToString(),
                        EventStartDate = Convert.ToDateTime(reader["EventStartDate"]),
                        EventSlutDate = Convert.ToDateTime(reader["EventSlutDate"]),
                        Deleted = Convert.ToInt32(reader["Deleted"])
                    });

                    batsamTemp.Samurais.Add(new SamuraiModel()
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Birthdate = Convert.ToDateTime(reader["Birthdate"]),
                        Deleted = Convert.ToInt32(reader)
                    });

                    //overfører den ene person til List
                    AllBatSams.Add(batsamTemp);
                }
                //returner Listen med Data
                return AllBatSams;
            }
        }

    }
}
