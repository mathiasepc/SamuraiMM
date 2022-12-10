using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    internal interface IQuotes
    {
        public void CreateQuote();
        public void InsertQuote(QuoteModel quote);
        public void DeleteQuote(int ID);
        public void UpdateQuote(QuoteModel quote);
        public List<QuoteModel> ReadAllQuotes();
    }
}
