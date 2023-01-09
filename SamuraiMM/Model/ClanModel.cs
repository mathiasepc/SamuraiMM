using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class ClanModel
    {
        public int ID { get; set; }
        //Hvis feltet er tomt
        [Required(ErrorMessage = "Clan name is required.")]
        //Længde på string 50
        [StringLength(50, ErrorMessage = "Clan name is too long.")]
        public string ClanName { get; set; }
        public List<SamuraiModel> Samurais { get; set; }
        public int Deleted { get; set; }
    }
}
