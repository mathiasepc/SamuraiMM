using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM
{
    internal class ADOHandler
    {
        /// <summary>
        /// opretteer min connection string til database
        /// </summary>
        public string ConnectionString
        {
            get => "Data Source=Martin-PC;Initial Catalog=TEC;Integrated Security=True";
        }
    }
}
