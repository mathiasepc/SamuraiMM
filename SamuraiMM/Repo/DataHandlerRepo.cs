using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SamuraiMM.Repo
{
    /// <summary>
    /// Vi bruger indtil videre intet. men til visning.
    /// </summary>
    internal class DataHandlerRepo : IDataHandler
    {
        //opretter klassen med connectionString
        ADOHandler ADO = new();

        SqlCommand Command = new();

        //laver mine globale variabler
        string ColumNamesBuild = string.Empty;
        string Injection = string.Empty;
        /*Da injection skal være en ny injection hver gang. 
              Laver en counter, så den skifter navn.*/
        int InjectionCounter = 0;

        /// <summary>
        /// Laver en dynamisk insert for alle CRUD
        /// </summary>
        /// <param name="model"></param>
        public void FilterData(object model)
        {
            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                //reseter data
                ResetData();

                connection.Open();

                //istansiere en variable som får navnet på modellen
                var entityName = model.GetType().Name;
                //fjerne model fra navnet så den macther entity.
                entityName = entityName[..^5];

                //går igennem objektet med data
                foreach (var item in model.GetType().GetProperties())
                {
                    //tjekker for id. Bruger .Name for at få navnet på kolonnen
                    bool resultIDCheck = CheckID(item.Name);

                    //Tilføj. Bruger .Name for ikke at få, feks., "Int32 ID" Men "ID".
                    //columNamesBuild += $"{item.Name},";

                    //hvis ikke det er ID. kør
                    if (resultIDCheck == true)
                    {
                        //henter typen af data udfra objectet(model) for at finde ud af om det er en string
                        if (item.GetValue(model) is string)
                        {
                            //Tilføj. Bruger .Name for ikke at få, feks., "Int32 ID" Men "ID".
                            ColumNamesBuild += $"{item.Name},";

                            SetStringDataSamurai($"{item.GetValue(model)}");
                        }
                        if (item.GetValue(model) is DateTime)
                        {
                            //Tilføj. Bruger .Name for ikke at få, feks., "Int32 ID" Men "ID".
                            ColumNamesBuild += $"{item.Name},";

                            var tempDate = (DateTime)item.GetValue(model);

                            SetDateTimeDataSamurai(tempDate);
                        }
                        //if (item.GetValue(model) is ICollection)
                        //{
                        //    //Tilføj. Bruger .Name for ikke at få, feks., "Int32 ID" Men "ID".
                        //    ColumNamesBuild += $"{item.Name},";

                        //    var samuraiModel = new SamuraiModel();

                        //    SqlDataReader reader = Command.ExecuteReader();

                        //    while (reader.Read())
                        //    {


                        //    }

                        //    //bygger min undgå sqlInjection ordenligt op
                        //    Injection += $"@{InjectionCounter},";
                        //    //for at min injection er forskellige stiger den med 1
                        //    InjectionCounter++;
                        //}
                    }
                }
                //kalder indsæt data metoden
                InsertData(ColumNamesBuild, Injection, entityName, connection);
            }
        }

        /// <summary>
        /// Laver en metode som indsætter i min database
        /// </summary>
        /// <param name="columNamesBuild"></param>
        /// <param name="injection"></param>
        /// <param name="entityName"></param>
        /// <param name="connection"></param>
        private void InsertData(string columNamesBuild, string injection, string entityName, SqlConnection connection)
        {
            //fjerner det sidste komma i strings. Den kan ikke slutte på et komma.
            string columNames = columNamesBuild.Remove(columNamesBuild.Length - 1);
            string values = injection.Remove(injection.Length - 1);

            //Klargør commando string
            string command = new($"INSERT INTO {entityName}({columNames}) values({values.ToString()})");

            //indsætter commando string
            Command.CommandText = command;
            Command.Connection = connection;

            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Laver en metode som Conventere DateTime og undgår sqlinjection
        /// </summary>
        /// <param name="item"></param>
        private void SetDateTimeDataSamurai(DateTime item)
        {
            //Tilføj. får valuen ud af propperties med .GetValue()
            DateTime value = Convert.ToDateTime(item);

            //undgår SQL injection for data
            Command.Parameters.AddWithValue($"@{InjectionCounter}", value);

            //bygger "undgå" sqlInjection ordenligt op
            Injection += $"@{InjectionCounter},";
            //for at injection er forskellige stiger den med 1
            InjectionCounter++;
        }

        /// <summary>
        /// Laver en metode som undgår sqlinjection på strings
        /// </summary>
        /// <param name="value"></param>
        private void SetStringDataSamurai(string value)
        {
            //undgår SQL injection for data
            Command.Parameters.AddWithValue($"@{InjectionCounter}", value);

            //bygger "undgå" sqlInjection ordenligt op
            Injection += $"@{InjectionCounter},";
            //for at injection er forskellige stiger den med 1
            InjectionCounter++;
        }

        /// <summary>
        /// laver en checker for det data vi skal arbejde med.
        /// Data'en må ikke være ID da den er quto increment.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CheckID(object item)
        {
            //Hvis ikke det er ID skal den være true
            bool answerID = true;

            //laver en if for hvis det id så det kan være false ellers true
            if (item.ToString() == "ID") { answerID = false; }
            else { answerID = true; }

            return answerID;
        }

        /// <summary>
        /// Laver en metode som reseter min data
        /// </summary>
        private void ResetData()
        {
            ColumNamesBuild = string.Empty;
            Injection = string.Empty;
            Command = new();
        }
    }
}
