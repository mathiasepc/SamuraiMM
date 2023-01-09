using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiMM.Interfaces
{
    public interface IQuotes
    {
        public void CreateTableQuote();
        public void InsertQuote(QuoteModel quote);
        public void DeleteQuote(int ID);
        public void UpdateQuote(QuoteModel quote);
        public List<QuoteModel> ReadAllQuotes();
        public QuoteModel ReadOneQuote(int quoteID);
        public List<QuoteModel> ReadAllQuotesWithSamuraiName();
    }
}
