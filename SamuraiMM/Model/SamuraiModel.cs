using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class SamuraiModel
    {
        public int ID { get; set; }
        public int ClanID { get; set; }
        //Hvis feltet er tomt 
        [Required(ErrorMessage = "Firstname is required.")]
        //Laver længden på string 50
        [StringLength(50, ErrorMessage = "Firstname is too long.")]
        public string FirstName { get; set; }
        //Hvis feltet er tomt skriv
        [Required(ErrorMessage = "Lastname is required.")]
        //Laver længden på string 50
        [StringLength(50, ErrorMessage = "Lastname is too long.")]
        public string LastName { get; set; }
        //Da Date er en struct(Value Type) og ikke en reference type. Har den som default en value. Derfor laver vi en range i stedet
        [Range(typeof(DateTime), "01/01/1753", "01/01/9999", ErrorMessage = "Date is out of Range: 01/01/1753-01/01/9999")]
        public DateTime Birthdate { get; set; }
        public int Deleted { get; set; }
        public HorseModel Horse { get; set; }
        public List<BladeModel> Blades { get; set; }
        public List<QuoteModel> Quotes { get; set; }
        public ClanModel Clan { get; set; } 
        public List<BattleModel> Battles { get; set; }  
    }
}
