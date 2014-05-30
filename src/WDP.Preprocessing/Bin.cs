using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class Bin
    {
        public Bin()
        {
            Bids = new List<Bid>();
        }
        public List<Bid> Bids { get; set; }
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
        public  string ToDMAString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < Bids.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(Bids[i].Id);
            }
            sb.Append("]");
            return sb.ToString();
        }
        public string Encoded()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < Bids.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(Bids[i].Encoded());
            }
            sb.Append("]");
            return sb.ToString();
        }

        internal void EncodeBids(int m)
        {
            foreach (var bid in Bids)
                bid.EncodeBid(m);
        }
    }
}
