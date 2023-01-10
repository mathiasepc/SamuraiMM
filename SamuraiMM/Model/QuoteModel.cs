using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    public class QuoteModel
    {
        public int ID { get; set; }
        //Hvis feltet er tomt skriv
        [Required(ErrorMessage = "Quote is required.")]
        //Laver længden på string 50
        [StringLength(50, ErrorMessage = "Quote is too long.")]
        public string QuoteText { get; set; }
        //Laver en range for Int 1 - max int value
        [Range(1, int.MaxValue, ErrorMessage = "You need to pick a samurai.")]
        public int SamuraiID { get; set; }
        public SamuraiModel Samurai { get; set; }
    }
}
