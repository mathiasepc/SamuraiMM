using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    internal interface IBlade
    {
        public void CreateTableBlade();
        public void InsertBlade(BladeModel blade);
        public void DeleteBlade(int ID);
        public void UpdateBlade(BladeModel blade);
        public List<BladeModel> ReadAllBlades();
    }
}
