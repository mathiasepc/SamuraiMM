using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class HorseModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Horse race is required.")]
        [StringLength(50, ErrorMessage = "Horse race is too long.")]
        public string? HorseRace { get; set; }

        public int SamuraiID { get; set; }
    }
}
