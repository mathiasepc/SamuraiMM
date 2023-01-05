using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class ClanRepo : IClan
    {
        ADOHandler ADO = new();

        public void CreateTableClan()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Clan(ID int Identity(1,1) Primary Key, ClanName nvarchar(50), SamuraiID int Foreign KEY references Samurai(ID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="quote"></param>
        public void InsertClan(ClanModel clan)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Clan (ClanName, SamuraiID) values('{clan.ClanName}', '{clan.SamuraiID}')", sqlConnection);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteClan(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from Clan Where ID ='{ID}'");

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
        public void UpdateClan(ClanModel clan)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE Clan SET FirstName = '{clan.ClanName}', SamuraiId = {clan.SamuraiID} Where ID = {clan.ID}", sqlConnection);

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        public ClanModel ReadOneClan(int clanID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Blade where id={clanID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                ClanModel cla = new ClanModel();

                //de forskellige værdier fra databasen
                cla.ID = Convert.ToInt32(reader["id"]);
                cla.ClanName = reader["ClanName"].ToString();

                //returner den nye model
                return cla;
            }
        }

        public List<ClanModel> ReadAllClans()
        {
            //vi laver en list som vi indsætter data'en i
            List<ClanModel> allClans = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Clan", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    ClanModel clanTemp = new ClanModel() { ID = reader.GetInt32(0), ClanName = reader.GetString(1), SamuraiID = reader.GetInt32(2) };

                    //overfører den ene person til List
                    allClans.Add(clanTemp);
                }
                //returner Listen med Data
                return allClans;
            }
        }
    }
}
