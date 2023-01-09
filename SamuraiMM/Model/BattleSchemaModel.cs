using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class BattleSchemaModel
    {
        //Hvis feltet er tomt 
        [Required(ErrorMessage = "SamuraiID is required.")]
        public int SamuraiID { get; set; }
        public List<SamuraiModel> Samurais { get; set; }
        //Hvis feltet er tomt 
        [Required(ErrorMessage = "BattlesID is required.")]
        public int BattlesID { get; set; }
        public List<BattleModel> Battles { get; set; }
        public SamuraiModel samuraiModel { get; set; }
    }
}
