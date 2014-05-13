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
