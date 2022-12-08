using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    internal interface ISamurai 
    {
        public void CreateSamurai();

        public void InsertSamurai(SamuraiModel samurai);
        public void DeleteSamurai(int SamuraiID);
    }
}
