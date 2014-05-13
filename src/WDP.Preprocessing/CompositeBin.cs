using System.Collections.Generic;
using System.Linq;

namespace WDP.Preprocessing
{
    public class CompositeBin
    {
        public CompositeBin()
        {
            Bids = new List<CompositeBid>();
        }
        public CompositeBin(Bin bin)
        {
            Bids = bin.Bids.Select(bid=>new CompositeBid(bid)).ToList();
            
        }
        public List<CompositeBid> Bids { get; set; }
        public override string ToString()
        {
            string result = "[";
            for (int i = 0; i < Bids.Count; i++)
            {
                if (i > 0) result += ",";
                result += Bids[i].ToString();
            }
            result += "]";
            return result;
        }
    }
}