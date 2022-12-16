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
        public string ConnectionString
        {
            get => "Data Source=Martin-PC;Initial Catalog=TEC;Integrated Security=True";
        }
    }
}
