using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class DataHandlerModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SamuraiID { get; set; }
        public int HorseID { get; set; }
        //hestens samuraiID
        public int HorsesSamuraiID { get; set; }
    }
}
