﻿using SamuraiMM.Model;
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
                SqlCommand command = new SqlCommand($"CREATE TABLE Horse(ID int Identity(1,1) Primary Key, Firstname nvarchar(50), SamuraiID int Foreign KEY references Samurai(ID)); ", sqlConnection);

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
                SqlCommand sqlCommand = new($"INSERT INTO Horse (Firstname, SamuraiID) values('{horse.FirstName}', '{horse.SamuraiID}')", sqlConnection);


                //sender til min database
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteHorse(int ID)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner for min connection
                sqlConnection.Open();

                //laver en string som fortæller hvad sql skal gøre
                string sqlCommand = new($"Delete from Horse Where ID ='{ID}'");

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
        public void UpdateHorse(HorseModel horse)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en SQLCommando for at update databasen og indsætter sqlConnection
                SqlCommand commandChange = new($"UPDATE Horse SET FirstName = '{horse.FirstName}', SamuraiId = {horse.SamuraiID} Where ID = {horse.ID}", sqlConnection);

                //eksekver
                commandChange.ExecuteNonQuery();
            }
        }

        public List<HorseModel> ReadAllHorses()
        {
            //vi laver en list som vi indsætter data'en i
            List<HorseModel> allHorses = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando
                SqlCommand command = new SqlCommand("SELECT * FROM Horse", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    HorseModel horseTemp = new HorseModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), SamuraiID = reader.GetInt32(3)};

                    //overfører den ene person til List
                    allHorses.Add(horseTemp);
                }
                //returner Listen med Data
                return allHorses;
            }
        }
    }
}
