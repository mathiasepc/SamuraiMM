using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SamuraiMM
{
    //hej
    internal class ADOHandler
    {
        /// <summary>
        /// índsætter connection til database i en string
        /// </summary>
        public string ConnectionString
        {
            get => "Data Source=DESKTOP-GV81FRQ\\TECH2DATABASE;Initial Catalog=SamuraiEksamen;Integrated Security=True";
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Opretter tabellen
        /// </summary>
        public void CreateDataBase()
        {
            using (SqlConnection sqlConnection = new(ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new("CREATE TABLE ADOHandler(ID int, Firstname nvarchar(50), Lastname nvarchar(50), HorsesSamuraiID int, SamuraiID int) ", sqlConnection);

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
                ADOModel adoM = new()
                {
                    SamuraiID = samuraiModel.ID,
                    Firstname = samuraiModel.FirstName,
                    Lastname = samuraiModel.LastName,
                    ID = ID
                };
                //overfører data til at kunne indsætte
                InsertIntoADODatabase(adoM);
            }
            else/*Måske smartere at bruge if? spørg flemming*/
            {
                //indsætter data i mit objekt
                ADOModel adoM = new()
                {
                    HorseID = horseModel.ID,
                    Firstname = horseModel.FirstName,
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
        /// <param name="adoM"></param>
        public void InsertIntoADODatabase(ADOModel adoM)
        {
            using (SqlConnection sqlConnection = new(ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new($"INSERT INTO ADOHandler(ID, Firstname, Lastname, SamuraiID, HorseID) values('{adoM.ID}', '{adoM.Firstname}', '{adoM.Lastname}', '{adoM.SamuraiID}', '{adoM.HorsesSamuraiID}')", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteADODatabase(int DatabaseID)
        {
            using (SqlConnection sqlConnection = new(ConnectionString))
            {
                sqlConnection.Open();

                //gør klar commandoSting klar til at kunne slette
                string sqlCommand = new($"Delete From ADOHandler where ID='{DatabaseID}'");

                //Gør klar til sletning
                SqlDataAdapter sqlDataAdapter = new();

                //putter min sql commandoString og connectionstring i deleteCommand
                sqlDataAdapter.DeleteCommand = new(sqlCommand, sqlConnection);

                //eksekverer commandoen og sletter rækken.
                sqlDataAdapter.DeleteCommand.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// laver et eksempel for en filter metode
        /// </summary>
        public List<ADOModel> FilterDataBase()
        {
            List<ADOModel> FilteredData = new(ADOModels.Where(x => x.ID >= 0));

            return FilteredData;
        }
>>>>>>> master
    }
}
