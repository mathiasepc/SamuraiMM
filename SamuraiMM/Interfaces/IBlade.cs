using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IBlade
    { 
        public void InsertBlade(BladeModel blade);
        public void DeleteBlade(int ID);
        public void UpdateBlade(BladeModel blade);
        public BladeModel ReadOneBlade(int bladeID);
        public List<BladeModel> ReadAllBladesAndSamurais();
    }
}
