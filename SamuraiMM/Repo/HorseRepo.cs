using SamuraiMM.Interfaces;
using SamuraiMM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class HorseRepo : IHorse
    {
        ADOHandler ADO = new();

        public void CreateTableHorse()
        {
            //fortæller hvad connectionen er til min database
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for connection
                sqlConnection.Open();

                //Fortæller hvad den skal gøre i SQL
                SqlCommand command = new SqlCommand($"CREATE TABLE Horse(Name nvarchar(50), HorseRace nvarchar(50), SamuraiID int Foreign KEY references Samurai(ID), CONSTRAINT Horse_PK PRIMARY KEY(SamuraiID));", sqlConnection);

                //opretter tablen
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Den smadre hele tabllen (horse) og indsætter et tomt felt i LastName
        /// 
        /// dvs. der du injecters felt forbliver tomt og den table som står i truncate smadres
        /// </summary>
        public void InsertWithInjection()
        {
            string samuraiCMD = "'); truncate table Samurai; --";
            SqlConnection con = new SqlConnection(ADO.ConnectionString);
            SqlCommand cmd = new SqlCommand($"Insert into Horse(Name, HorseRace) values ('Mathias','{samuraiCMD}' )", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Uden avoidinjection
        /// </summary>
        /// <param name="horse"></param>
        public void InsertHorse(HorseModel horse)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();
                SqlCommand sqlCommand = new();

                //istansiere SqlCommand klassen og indsætter i databasen
                sqlCommand = new($"INSERT INTO Horse (Name, SamuraiID, HorseRace) values('{horse.Name}', '{horse.SamuraiID}', '{horse.HorseRace}')", sqlConnection);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// med avoid injection
        /// </summary>
        /// <param name="horse"></param>
        public void InsertHorseAvoidInjection(HorseModel horse)/*Kan bare base CarModel i stedet for alle propperty i Modellen.*/
        {
            //laver en vej til min server bruger using for at den selv lukker.
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand sqlCommand = new();

                //istansiere samurai klassen
                SamuraiRepo s = new();

                //henter døde samurai
                var samlist = s.ReadAllAliveSamurais();

                foreach (var samurai in samlist)
                {
                    //hvis indtastet er forskellige for død samurai
                    if (horse.SamuraiID == samurai.ID)
                    {
                        //istansiere SqlCommand klassen og indsætter i databasen
                        sqlCommand = new($"INSERT INTO Horse (Name, SamuraiID, HorseRace) values(@f1, @f2, @f3)", sqlConnection);
                    }
                }
                sqlCommand.Parameters.AddWithValue("@f1", horse.Name);
                sqlCommand.Parameters.AddWithValue("@f2", horse.SamuraiID);
                sqlCommand.Parameters.AddWithValue("@f3", horse.HorseRace);

                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// metode der kan slette en hest
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteHorse(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from Horse Where SamuraiID ='{ID}'");

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
        public void UpdateHorse(HorseModel horse, int id)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                SqlCommand commandChange = new();

                //istansiere samurai klassen
                SamuraiRepo s = new();

                //henter døde samurai
                var aliveSamurai = s.ReadAllAliveSamurais();

                foreach (var alive in aliveSamurai)
                {
                    //hvis indtastet er forskellige for død samurai
                    if (horse.SamuraiID == alive.ID)
                    {
                        //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                        commandChange = new($"UPDATE Horse SET Name = '{horse.Name}', SamuraiID = '{horse.SamuraiID}', HorseRace = '{horse.HorseRace}' Where SamuraiID = {id}", sqlConnection);
                    }
                }
                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// metode for at læse en hest
        /// </summary>
        /// <param name="horseSamuraiID"></param>
        /// <returns></returns>
        public HorseModel ReadOneHorse(int horseSamuraiID)
        {
            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //laver en sql commando
                SqlCommand cmd = new SqlCommand($"select * from Horse where SamuraiID={horseSamuraiID}", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = cmd.ExecuteReader();

                //læser dataen
                reader.Read();

                //vi laver en nu model hvor vi indsætter værdierne
                HorseModel hor = new HorseModel();

                //de forskellige værdier fra databasen
                hor.SamuraiID = Convert.ToInt32(reader["SamuraiID"]);
                hor.Name = reader["Name"].ToString();
                hor.HorseRace = reader["HorseRace"].ToString();

                //returner den nye model
                return hor;
            }
        }

        /// <summary>
        /// Læser alle heste med samurai navne i
        /// </summary>
        /// <returns></returns>
        public List<HorseModel> ReadAllHorsesAndSamurai()
        {
            //vi laver en list som vi indsætter data'en i
            List<HorseModel> allHorses = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Horse JOIN Samurai ON Samurai.ID = SamuraiID", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    HorseModel horseTemp = new();

                    horseTemp.SamuraiID = Convert.ToInt32(reader["SamuraiID"]);
                    horseTemp.Name = reader["Name"].ToString();
                    horseTemp.HorseRace = reader["HorseRace"].ToString();
                    horseTemp.Samurai = new SamuraiModel() { FirstName = reader["FirstName"].ToString(), LastName = reader["LastName"].ToString(), Deleted = Convert.ToInt32(reader["Deleted"]) };
                    //overfører den ene person til List
                    allHorses.Add(horseTemp);
                }
                //returner Listen med Data
                return allHorses;
            }
        }
    }
}
