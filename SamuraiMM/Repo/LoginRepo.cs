using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SamuraiMM.Repo
{
    public class LoginRepo : ILogin
    {
        private LoginModel loginModel = new();
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

        public bool ValidateEmail(string Email)
        {
            string regexEmail = @"^[a-zA-Z0-9.+]+@[a-zA-Z0-9.-]+.[a-zA-Z0-9]{2,4}$";
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

        public void CreateLogin(LoginModel loginModel)
        {
            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                List<LoginModel> loginList = new(GetAllUsers());
                SqlCommand command = new();
                connection.Open();

                //opretter en temporary liste for at compare
                List<LoginModel> sameMail = new();

                //Tilføjer filtered værdi til temp liste
                sameMail.Add(loginList.FirstOrDefault(f => f.Email == loginModel.Email));

                foreach (var item in sameMail)
                {
                    //Opretter hvis tempmail listen er større end 0 og item er null eller hvis emailen ikke allerede findes
                    if (sameMail.Count() > 0 && item == null || item.Email != loginModel.Email)
                    {
                        command = new($"Insert into Login(Email,Password,UserSession) values('{loginModel.Email}', '{loginModel.Password}', '0')", connection);
                    }
                }

                //Forcer crash hvis email eksisterer allerede
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Laver en metode for at logge ind
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool GetUser(LoginModel tempLoginModel)
        {
            //svaret for om man kan logge ind
            bool answer = false;
            loginModel.Email = tempLoginModel.Email;
            loginModel.Password = tempLoginModel.Password;

            using (SqlConnection connection = new(ADO.ConnectionString))
            {
                try
                {
                    connection.Open();

                    //henter user
                    SqlCommand command = new($"SELECT Email, Password FROM Login where Email='{loginModel.Email}' and Password='{loginModel.Password}';", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();

                    //læser user
                    string? emailTemp = (string)reader["Email"].ToString();
                    string? passwordTemp = (string)reader["Password"].ToString();

                    //matcher indtastet data med data'en i data for at se om man eksistere
                    if (emailTemp == loginModel.Email && passwordTemp == loginModel.Password)
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
                SqlCommand command = new($"UPDATE Login SET UserSession = 1 WHERE Password='{loginModel.Password}' and Email='{loginModel.Email}'", connection);

                //opdaterer UserSession
                UserSession = 1;

                command.BeginExecuteNonQuery();
            }
        }

        /// <summary>
        /// laver en metode for at logge ud
        /// </summary>
        /// <returns></returns>
        public string UserLogOut(LoginModel loginModel)
        {
            if (UserSession == 1)
            {
                using (SqlConnection connection = new(ADO.ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new($"UPDATE Login SET UserSession = 0 WHERE Password={loginModel.Password} and Email={loginModel.Email}", connection);

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
        public List<LoginModel> GetAllUsers()
        {
            //vi laver en list som vi indsætter data'en i
            List<LoginModel> allLogin = new();

            using (SqlConnection con = new SqlConnection(ADO.ConnectionString))
            {
                con.Open();

                //Laver en SqlCommando der henter quotes samt navne fra samurai
                SqlCommand command = new SqlCommand("SELECT * FROM Login", con);

                //vi bruger SqlDataReader for at kunne læse data'en fra databasen hvor vi indsætter vores commando
                SqlDataReader reader = command.ExecuteReader();

                //laver et while loop for at få alt data fra databasen
                while (reader.Read())
                {
                    //laver en midlertidig model for at kunne overfører den ene person til vores List
                    LoginModel tempLogin = new()
                    {
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    };

                    //overfører den ene person til List
                    allLogin.Add(tempLogin);
                }
                //returner Listen med Data
                return allLogin;
            }
        }
    }
}

