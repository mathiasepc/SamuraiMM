﻿using SamuraiMM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiMM.Model;

namespace SamuraiMM.Repo
{
    internal class SamuraiRepo : ISamurai
    {
        public ADOHandler ADO = new();
        //istansiere min list og Model.
        public List<SamuraiModel> Samurai { get; set; }
        SamuraiModel samuraiM = new();
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

        public void UpdateSamurai(SamuraiModel samurai)
        {
            using (SqlConnection sqlConnection = new(ADO.ConnectionString))
            {
                //åbner vejen
                sqlConnection.Open();

                //Laver en string med Commando
                string change = $"UPDATE Samurai SET FirstName = '{samurai.FirstName}', LastName = '{samurai.LastName}', Birthdate = {samurai.Birthdate} Where ID = {samurai.ID}";

                //laver en adapter til sql sådan så den kan opdaterer min tabel
                SqlDataAdapter sqlDataAdapter = new();

                //putter min sql commando og connectionstring i deleteCommand
                sqlDataAdapter.DeleteCommand = new(change, sqlConnection);

                //eksekverer commandoen
                sqlDataAdapter.DeleteCommand.ExecuteNonQuery();

                //sletter rækken
                sqlDataAdapter.Dispose();
            }
        }

        public SamuraiModel ReadSamurai(int samuraiID)
        {
            SqlConnection con = new SqlConnection(ADO.ConnectionString);
            SqlCommand cmd = new SqlCommand($"select * from Samurai where id={samuraiID}", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            SamuraiModel sam = new SamuraiModel();
            sam.ID = Convert.ToInt32(reader["id"]);
            sam.FirstName = reader["FirstName"].ToString();
            sam.LastName = reader["LastName"].ToString();
            sam.Birthdate = Convert.ToDateTime(reader["BirthDate"]);
            con.Close();
            return sam;
        }
        public List<SamuraiModel> ReadSamurais()
        {
            List<SamuraiModel> samurais = new List<SamuraiModel>();

            SqlConnection con = new SqlConnection(ADO.ConnectionString);
            con.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Samurai", con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                SamuraiModel sam = new SamuraiModel() { ID = reader.GetInt32(0), FirstName = reader.GetString(1), LastName = reader.GetString(2), Birthdate = reader.GetDateTime(3) };
                samurais.Add(sam);
            }

            con.Close();
            return samurais;
        }
    }
}
    
