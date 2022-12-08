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
        
        /// <summary>
        /// opretter connection string til database
        /// </summary>
        public string ConnectionString
        {
            get => "Data Source=DESKTOP-GV81FRQ\\TECH2DATABASE;Initial Catalog=SamuraiEksamen;Integrated Security=True";
        }

        public void CreateDataBase()
        {
            using (SqlConnection sqlConnection= new(ConnectionString)) { 
                sqlConnection.Open();

                SqlCommand sqlCommand = new("CREATE TABLE ADOHandler(ID int, Firstname nvarchar(50), Lastname nvarchar(50)) ", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void InsertIntoADOModel(SamuraiModel samuraiModel, HorseModel horseModel)
        {
            if(samuraiModel != null)
            {
                ADOModel adoM = new()
                {
                    ID = samuraiModel.ID,
                    Firstname = samuraiModel.FirstName,
                    Lastname = samuraiModel.LastName
                };
                InsertIntoADOModel(adoM);   
            }
            else
            {
                ADOModel adoM = new()
                {
                    ID = horseModel.ID,
                    Firstname = horseModel.Firstname
                };
                InsertIntoADOModel(adoM);
            }

        }

        public void InsertIntoADOModel(ADOModel adoM)
        {
            using (SqlConnection sqlConnection= new(ConnectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new($"INSERT INTO ADOHandler(ID, Firstname, Lastname) values('{adoM.ID}', '{adoM.Firstname}', '{adoM.Lastname}')", sqlConnection);

                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
