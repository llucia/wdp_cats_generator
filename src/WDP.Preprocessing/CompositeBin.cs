using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < Bids.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(Bids[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}