﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IDataHandler
    {
        public void CreateDataBase();
        public void DeleteADODatabase(int DatabaseID);
    }
}
