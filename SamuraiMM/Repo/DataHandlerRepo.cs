using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    internal class DataHandler
    {
        //opretter klassen med connectionString
        ADOHandler ado = new();

        /// <summary>
        /// Opretter tabellen
        /// </summary>
        public void CreateDataBase()
        {
            using (SqlConnection sqlConnection = new(ado.ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new("CREATE TABLE DataModel(ID int, FirstName nvarchar(50), LastName nvarchar(50), HorsesSamuraiID int, SamuraiID int) ", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void FilterInsertADOModel2(object model)
        {
            var temp = model;
            //string SQL = $"INSERT INTO {model.GetType().Name}(test) values('{test2}')";
            foreach (var item in model.GetType().GetProperties())
            {
                var test = item;
                var test2 = item.GetValue(model);
                //var propperty = model.GetType().GetProperty($"{test}").GetValue(model);
            }

            //{((SamuraiModel)model).Firstname}')";

            //string SQL = $"INSERT INTO Horse(Firstname) values('{((HorseModel)model).Firstname}')";
        }
        /// <summary>
        /// laver en metode som filter input
        /// </summary>
        /// <param name="samuraiModel"></param>
        /// <param name="horseModel"></param>
        public void FilterInsertADOModel(SamuraiModel samuraiModel, HorseModel horseModel, int ID)
        {
            //tjekker indput
            if (samuraiModel != null)
            {
                //indsætter data i mit objekt
                DataHandlerModel adoM = new()
                {
                    SamuraiID = samuraiModel.ID,
                    FirstName = samuraiModel.FirstName,
                    LastName = samuraiModel.LastName,
                    ID = ID
                };
                //overfører data til at kunne indsætte
                InsertIntoADODatabase(adoM);
            }
            else/*Måske smartere at bruge if? spørg flemming*/
            {
                //indsætter data i mit objekt
                DataHandlerModel adoM = new()
                {
                    HorseID = horseModel.ID,
                    FirstName = horseModel.FirstName,
                    HorsesSamuraiID = horseModel.SamuraiID,
                    ID = ID
                };
                //overfører data til at kunne indsætte
                InsertIntoADODatabase(adoM);
            }
        }
        /// <summary>
        /// Metoden som indsætter i databasen Bliver kaldt af FilterInsertADOModel()
        /// </summary>
        /// <param name="inserDataInDataModel"></param>
        public void InsertIntoADODatabase(DataHandlerModel inserDataInDataModel)
        {
            using (SqlConnection sqlConnection = new(ado.ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new($"INSERT INTO DataModel(ID, Firstname, Lastname, SamuraiID, HorseID) values('{inserDataInDataModel.ID}', '{inserDataInDataModel.FirstName}', '{inserDataInDataModel.LastName}', '{inserDataInDataModel.SamuraiID}', '{inserDataInDataModel.HorsesSamuraiID}')", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteADODatabase(int DatabaseID)
        {
            using (SqlConnection sqlConnection = new(ado.ConnectionString))
            {
                sqlConnection.Open();

                //gør klar commandoSting klar til at kunne slette
                string sqlCommand = new($"Delete From DataModel where ID='{DatabaseID}'");

                //Gør klar til sletning
                SqlDataAdapter sqlDataAdapter = new();

                //putter min sql commandoString og connectionstring i deleteCommand
                sqlDataAdapter.DeleteCommand = new(sqlCommand, sqlConnection);

                //eksekverer commandoen og sletter rækken.
                sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
            }
        }
    }
}
