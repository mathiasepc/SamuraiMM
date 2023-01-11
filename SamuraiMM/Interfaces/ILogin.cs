using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface ILogin
    {
        public string UserLogOut();
        public bool GetUser(LoginModel loginModel);
        public void CreateLogin(string email, string password);
        public bool ValidateEmail(string Email);
        public List<LoginModel> GetAllUsers();
    }
}
