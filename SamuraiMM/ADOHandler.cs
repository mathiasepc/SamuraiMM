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
        /// 
        public string ConnectionString
        {
            get => "Data Source=DESKTOP-GV81FRQ\\MSSQLSERVER01;Initial Catalog=EksamenOpgave;Integrated Security=True";
        }
    }
}
