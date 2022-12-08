using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SamuraiMM
{
    internal class ADOHandler
    {
        //laver en list til at kontrollere ting med
        private List<ADOModel> ADOModels { get; set; }

        public ADOHandler()
        {
            //refresher min liste hver gang klassen bliver kaldt
            UpdateListWithData();
        }

        /// <summary>
        /// índsætter connection til database i en string
        /// </summary>
        public string ConnectionString
        {
            get => "Data Source=DESKTOP-GV81FRQ\\TECH2DATABASE;Initial Catalog=SamuraiEksamen;Integrated Security=True";
        }

        /// <summary>
        /// Opretter tabellen
        /// </summary>
        public void CreateDataBase()
        {
            using (SqlConnection sqlConnection = new(ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new("CREATE TABLE ADOHandler(ID int, Firstname nvarchar(50), Lastname nvarchar(50)) ", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode som opdatere i en Liste hvad der er i databasen.
        /// </summary>
        public void UpdateListWithData()
        {
            //refresher liste
            ADOModels = new List<ADOModel>();
            //instansiere objekt
            ADOModel adoModel = new ADOModel();

            //Bruger using for automatisk luk. og gør klar til at connecte i min database
            using (SqlConnection connectionString = new SqlConnection(ConnectionString))
            {
                //åbner forbindelse
                connectionString.Open();

                //vælger alt fra ADOHandler
                SqlCommand sqlCommand = new SqlCommand("Select * from ADOHandler", connectionString);

                //så den kan læse dataen
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                //jeg kører den i while så den får læst alt
                while (sqlDataReader.Read())
                {
                    //tilføj data til objekt
                    adoModel.ID = Convert.ToInt32(sqlDataReader["id"]);
                    adoModel.Firstname = sqlDataReader["FirstName"].ToString();
                    adoModel.Lastname = sqlDataReader["LastName"].ToString();

                    //tilføj obejkt til liste
                    ADOModels.Add(adoModel);
                }
            }
        }

        /// <summary>
        /// laver en metode som filter input
        /// </summary>
        /// <param name="samuraiModel"></param>
        /// <param name="horseModel"></param>
        public void FilterInsertADOModel(SamuraiModel samuraiModel, HorseModel horseModel)
        {
            //tjekker indput
            if (samuraiModel != null)
            {
                //indsætter data i mit objekt
                ADOModel adoM = new()
                {
                    SamuraiID = samuraiModel.ID,
                    Firstname = samuraiModel.FirstName,
                    Lastname = samuraiModel.LastName
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
                    Firstname = horseModel.Firstname,
                    HorsesSamuraiID = horseModel.SamuraiID
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

                SqlCommand sqlCommand = new($"INSERT INTO ADOHandler(ID, Firstname, Lastname) values('{adoM.ID}', '{adoM.Firstname}', '{adoM.Lastname}')", sqlConnection);

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
        /// laver et filter for update
        /// </summary>
        public List<ADOModel> FilterDataBase()
        {
            List<ADOModel> FilteredData = new(ADOModels.Where(x => x.ID >= 0));

            return FilteredData;
        }
    }
}
