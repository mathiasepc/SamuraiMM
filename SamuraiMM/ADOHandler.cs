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
        /// opretter connection string til database
        /// </summary>
        public string ConnectionString
        {
            get => "DData Source=DESKTOP-GV81FRQ\\TECH2DATABASE;Initial Catalog=SamuraiEksamen;Integrated Security=True";
        }
    }
}
