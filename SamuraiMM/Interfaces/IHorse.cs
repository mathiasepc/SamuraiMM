using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IHorse
    {
        public void InsertHorseAvoidInjection(HorseModel horse);
        public void DeleteHorse(int ID);
        public void UpdateHorse(HorseModel horse, int ID);
        public HorseModel ReadOneHorse(int ID);
        public List<HorseModel> ReadAllHorsesAndSamurai();
    }
}
