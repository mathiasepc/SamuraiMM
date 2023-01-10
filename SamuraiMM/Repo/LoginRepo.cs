using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class Login : ILogin
    {
        private string Email = string.Empty;
        private string Password = string.Empty;
        //hvis UserSession = 0 er man logget ud. UserSession = 1 logget ind.
        private int UserSession = 0;

        ADOHandler ADO = new();
        
        public void CreateTableLogin()
        {
            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new("Create table Login(ID int identity(1,1) primary key, Email nvarchar(100),Password nvarchar(100), UserSession int)", connection);

                command.ExecuteNonQuery();
            }
        }

        private bool ValidateEmail(string Email)
        {
            string regexEmail = "^[a-zA-Z0-9.+]+@[a-zA-Z0-9.-]+.[a-zA-z0-9]{2,4}$";
            Regex reg = new Regex(regexEmail);

            if (reg.IsMatch(Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateLogin(string email, string password)
        {
            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                connection.Open();

                SqlCommand command = new($"Insert into Login(Email,Password,UserSession) values('{email}', '{password}', '0')", connection);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode for at logge ind
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool GetUser(string email, string password)
        {
            //svaret for om man kan logge ind
            bool answer = false;

            //overfører til globale variabler så vi kan logge ud igen
            Email = email;
            Password = password;

            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                try
                {
                    connection.Open();

                    //henter user
                    SqlCommand command = new($"SELECT Email, Password FROM Login where Email='{Email}' and Password='{Password}';", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();

                    //læser user
                    string? emailTemp = (string)reader["Email"].ToString();
                    string? passwordTemp = (string)reader["Password"].ToString();

                    //matcher indtastet data med data'en i data for at se om man eksistere
                    if (emailTemp == Email && passwordTemp == Password)
                    {
                        SetUserSession();

                        //hvis rigtig
                        answer = true;
                    }
                }
                catch
                {
                    //hvis forkert
                    answer = false;
                }
            }
            return answer;
        }

        /// <summary>
        /// laver en metode som ændre UserSession så man kan logge ud igen
        /// </summary>
        private void SetUserSession()
        {
            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                connection.Open();

                //opdatere i databasen
                SqlCommand command = new($"UPDATE Login SET UserSession = 1 WHERE Password={Password} and Email={Email}", connection);

                //opdaterer UserSession
                UserSession = 1;

                command.BeginExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode for at logge ud
        /// </summary>
        /// <returns></returns>
        public string UserLogOut()
        {
            if (UserSession == 1)
            {
                using (SqlConnection connection = new(ADO.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new($"UPDATE Login SET UserSession = 0 WHERE Password={Password} and Email={Email}", connection);

                    //sætter UserSession = 0 så man er logget ud
                    UserSession = 0;

                    command.BeginExecuteNonQuery();

                    return "Du nu logget ud";
                }
            }
            else
            {
                return "Du skal være logget ind først";
            }
        }
    }
}

