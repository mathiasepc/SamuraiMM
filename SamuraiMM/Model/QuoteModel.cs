using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Model
{
    internal class QuoteModel
    {
        public int ID { get; set; }
        public string QuoteText { get; set; }
        public int SamuraiId { get; set; }
    }
}
