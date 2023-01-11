﻿using SamuraiMM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface ILogin
    {
        public string UserLogOut(LoginModel loginModel);
        public bool GetUser(LoginModel loginModel);
        public void CreateLogin(LoginModel loginModel);
        public bool ValidateEmail(string Email);
        public List<LoginModel> GetAllUsers();
    }
}
