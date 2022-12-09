using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class BladeRepo
    {
        ADOHandler ADO = new();

        public void CreateBlade()
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

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Blade (Name, Description, SamuraiID) values('{blade.Name}', '{blade.Description}','{blade.SamuraiID}')", sqlConnection);

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
        public void UpdateClan(BladeModel blade)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE Blade SET Name = '{blade.Name}', Description = '{blade.Description}', SamuraiID = '{blade.SamuraiID}', Where ID = {blade.ID}", sqlConnection);

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        public List<BladeModel> ReadAllBlades()
        {
            //vi laver en list som vi indsætter data'en i
            List<BladeModel> allBlades = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Blade", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    BladeModel bladeTemp = new BladeModel() { ID = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), SamuraiID = reader.GetInt32(3) };

                    //overfører den ene person til List
                    allBlades.Add(bladeTemp);
                }
                //returner Listen med Data
                return allBlades;
            }
        }
    }
}
