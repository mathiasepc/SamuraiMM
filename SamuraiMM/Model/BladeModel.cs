using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class BladeModel
    {
        public int ID { get; set; }
        //Hvis feltet er tomt
        [Required(ErrorMessage = "Blade name is required.")]
        //Længde på string 50
        [StringLength(50, ErrorMessage = "Blade name is too long.")]
        public string Name { get; set; }
        //Hvis feltet er tomt
        [Required(ErrorMessage = "Description is required.")]
        //Længde på string 200
        [StringLength(200, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }
        //Laver en range for Int 1 - max int value
        [Range(1, int.MaxValue, ErrorMessage = "You need to pick a samurai.")]
        public int SamuraiID { get; set; }
        public SamuraiModel Samurai { get; set; }
    }
}
