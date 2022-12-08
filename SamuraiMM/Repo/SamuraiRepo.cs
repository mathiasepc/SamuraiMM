using SamuraiMM.Model;
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
        public ADOHandler ADO = new();
        //istansiere min list og Model.
        public List<SamuraiModel> Samurai { get; set; }
        SamuraiModel SamuraiM = new();

        public SamuraiRepo()
        {
            //instantiere min liste
            Samurai = new List<SamuraiModel>();
        }

        /// <summary>
        /// laver et table for biler
        /// </summary>
        public void CreateSamurai()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                using SqlCommand command = new SqlCommand($"CREATE TABLE Samurai(Id int, FirstName nvarchar(50), LastName nvarchar(50), Birthdate datetime); ", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// indsætter i min database
        /// </summary>
        /// <param name="car"></param>
        public void InsertSamurai(SamuraiModel samurai)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //istansiere SqlCommand klassen og indsætter i databasen
                SqlCommand sqlCommand = new($"INSERT INTO Samurai (Id, FirstName, LastName, Birthdate) values('{samurai.ID}', '{samurai.FirstName}', '{samurai.LastName}', '{samurai.Birthdate}')");

                //tilføjer min ConnectionString til sqlCommand object
                sqlCommand.Connection = sqlConnection;

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode som kan slette i databasen.
        /// </summary>
        /// <param name="carID"></param>
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

                //Laver en SQLCommando for at update databasen
                SqlCommand commandChange = new($"UPDATE Samurai SET FirstName = '{samurai.FirstName}', LastName = '{samurai.LastName}', Birthdate = @f3 Where ID = {samurai.ID}");

                //Da database ikke kan forstå datetime, parse vi den ind i en variable for sig.
                commandChange.Parameters.AddWithValue("@f3", samurai.Birthdate);

                //indsætter connection
                commandChange.Connection = sqlConnection;

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
            using SqlConnection con = new SqlConnection(ADO.ConnectionString);
            SqlCommand cmd = new SqlCommand($"select * from Samurai where id={samuraiID}", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            SamuraiModel sam = new SamuraiModel();
            sam.ID = Convert.ToInt32(reader["id"]);
            sam.FirstName = reader["FirstName"].ToString();
            sam.LastName = reader["LastName"].ToString();
            sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);
            return sam;
        }
        public List<SamuraiModel> ReadSamurais()
        {
            List<SamuraiModel> samurais = new();

            SqlConnection con = new SqlConnection(ADO.ConnectionString);
            con.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Samurai", con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SamuraiModel sam = new SamuraiModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Birthdate = reader.GetDateTime(3) };
                samurais.Add(sam);
            }
            return samurais;
=======
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Samurai where id={samuraiID}", con);

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
    }
}
    
