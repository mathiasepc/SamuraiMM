using SamuraiMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface ILogin
    {
        public void UserLogOut(LoginModel loginModel);
        public bool GetUser(LoginModel loginModel);
        public void CreateLogin(LoginModel loginModel);
    }
}
