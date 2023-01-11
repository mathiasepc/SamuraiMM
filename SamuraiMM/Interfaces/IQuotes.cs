using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IQuotes
    { 
        public void InsertQuote(QuoteModel quote);
        public void DeleteQuote(int ID);
        public void UpdateQuote(QuoteModel quote, int ID);
        public QuoteModel ReadOneQuote(int quoteID);
        public List<QuoteModel> ReadAllQuotesWithSamuraiName();
    }
}
