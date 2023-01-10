using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class BattleModel
    {
        public int ID { get; set; }
        //Hvis feltet er tomt
        [Required(ErrorMessage = "Event title is required.")]
        //Længde på string 50
        [StringLength(50, ErrorMessage = "Event title is too long.")]
        public string EventTitle { get; set; }
        //Hvis feltet er tomt
        [Required(ErrorMessage = "Description is required.")]
        //Længde på string 200
        [StringLength(200, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }
        //Da Date er en struct(Value Type) og ikke en reference type. Har som default en value. Derfor laver vi en range i stedet
        [Range(typeof(DateTime), "01/01/1753", "01/01/9999", ErrorMessage = "Date is out of Range: 01/01/1753-01/01/9999")]
        public DateTime EventStartDate { get; set; }
        //Da Date er en struct(Value Type) og ikke en reference type. Har som default en value. Derfor laver vi en range i stedet
        [Range(typeof(DateTime), "01/01/1753", "01/01/9999", ErrorMessage = "Date is out of Range: 01/01/1753-01/01/9999")]
        public DateTime EventSlutDate { get; set; }
        public List<SamuraiModel> Samurais { get; set; }
        public int Removed { get; set; }
    }
}
