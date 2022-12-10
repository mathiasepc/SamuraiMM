using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    internal interface IHorse
    {
        public void CreateTableHorse();
        public void InsertHorse(HorseModel horse);
        public void DeleteHorse(int ID);
        public void UpdateHorse(HorseModel horse);
        public List<HorseModel> ReadAllHorses();
    }
}
