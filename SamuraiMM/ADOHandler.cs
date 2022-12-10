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
        //laver en list til at kontrollere ting med
        public List<ADOModel> ADOModels { get; set; }

        /// <summary>
        /// índsætter connection til database i en string
        /// </summary>
        public string ConnectionString
        {
            get => "Data Source=DESKTOP-GV81FRQ\\TECH2DATABASE;Initial Catalog=SamuraiEksamen;Integrated Security=True";
        }
    }
}
