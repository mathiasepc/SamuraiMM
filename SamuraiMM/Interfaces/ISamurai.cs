using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface ISamurai
    {
        public void InsertSamurai(SamuraiModel samurai);
        public void DeleteSamurai(int SamuraiID);
        public void UpdateSamurai(SamuraiModel samurai);
        public SamuraiModel ReadOneSamurai(int samuraiID);
        public SamuraiModel ReadAliveSamuraisHouse(int samuraiID);
        public List<SamuraiModel> ReadAllSamurais();
        public List<SamuraiModel> ReadAllAliveSamurais();
        public SamuraiModel ReadOneAliveSamurai(int samuraiID);
    }
}
