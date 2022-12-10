using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    internal interface ISamurai
    {
        public void CreateTableSamurai();

        public void InsertSamurai(SamuraiModel samurai);
        public void DeleteSamurai(int SamuraiID);
        public void UpdateSamurai(SamuraiModel samurai);
        public SamuraiModel ReadOneSamurai(int samuraiID);
        public SamuraiModel ReadSamuraisHouse(int samuraiID);
        public List<SamuraiModel> ReadAllSamurais();
        public SamuraiModel ReadSamuraisQuotes(int samuraiID);
        public List<SamuraiModel> ReadAllSamuraiAndQuotes();
    }
}
