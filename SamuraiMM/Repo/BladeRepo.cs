using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class BladeRepo : IBlade
    {
        ADOHandler ADO = new();

        public void CreateTableBlade()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Blade(ID int Identity(1,1) Primary Key, Name nvarchar(50), Description nvarchar(200), SamuraiID int Foreign KEY references Samurai(ID)); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i tabellen Samurai
        /// </summary>
        /// <param name="quote"></param>
        public void InsertBlade(BladeModel blade)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand sqlCommand = new();

                //Vi henter Repo
                SamuraiRepo sam = new();

                //vi henter døde samurais
                var aliveSamurai = sam.ReadAllAliveSamurais();

                foreach (var alive in aliveSamurai)
                {
                    //hvis indtastet er forskellig for død samurai
                    if (blade.SamuraiID == alive.ID)
                    {
                        //istansiere SqlCommand klassen og indsætter i databasen
                        sqlCommand = new($"INSERT INTO Blade (Name, Description, SamuraiID) values('{blade.Name}', '{blade.Description}','{blade.SamuraiID}')", sqlConnection);

                    }
                }
                //sender til min database
                sqlCommand.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// vores metode for at slette
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteBlade(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from Blade Where ID ='{ID}'");

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
        public void UpdateBlade(BladeModel blade)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand commandChange = new();

                //Vi henter Repo
                SamuraiRepo sam = new();

                //vi henter døde samurais
                var aliveSamurai = sam.ReadAllAliveSamurais();

                foreach (var alive in aliveSamurai)
                {
                    //hvis indtastet er forskellig for død samurai
                    if (blade.SamuraiID == alive.ID)
                    {
                        //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                        commandChange = new($"UPDATE Blade SET Name = '{blade.Name}', Description = '{blade.Description}', SamuraiID = '{blade.SamuraiID}' Where ID = {blade.ID}", sqlConnection);
                    }
                }
                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// henter information om en blade
        /// </summary>
        /// <param name="bladeID"></param>
        /// <returns></returns>
        public BladeModel ReadOneBlade(int bladeID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Blade where id={bladeID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                BladeModel bla = new BladeModel();

                //de forskellige værdier fra databasen
                bla.ID = Convert.ToInt32(reader["id"]);
                bla.Name = reader["Name"].ToString();
                bla.Description = reader["Description"].ToString();

                //returner den nye model
                return bla;
            }
        }

        /// <summary>
        /// henter alle blades som er tilknyttet en samurai
        /// </summary>
        /// <returns></returns>
        public List<BladeModel> ReadAllBladesAndSamurais()
        {
            //vi laver en list som vi indsætter data'en i
            List<BladeModel> allBlades = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Blade JOIN Samurai ON Samurai.ID = SamuraiID", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    BladeModel bladeTemp = new BladeModel();

                    bladeTemp.ID = Convert.ToInt32(reader["id"]);
                    bladeTemp.Name = reader["Name"].ToString();
                    bladeTemp.Description = reader["Description"].ToString();
                    bladeTemp.Samurai = new SamuraiModel() { FirstName = reader["FirstName"].ToString(), LastName = reader["LastName"].ToString(), Deleted = Convert.ToInt32(reader["Deleted"]) };
                    //overfører den ene person til List
                    allBlades.Add(bladeTemp);
                }
                //returner Listen med Data
                return allBlades;
            }
        }
    }
}
