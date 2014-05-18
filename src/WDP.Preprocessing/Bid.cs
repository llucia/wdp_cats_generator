using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class Bid
    {
        public Bid()
        {
            Goods = new List<long>();
        }
        public List<long> Goods { get; set; }
        public double Value { get; set; }
        public long MinimumGood()
        {
            return Goods.Min();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for(int i=0; i<Goods.Count;i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(Goods[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
