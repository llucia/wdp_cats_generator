using System;
using System.Collections.Generic;
using System.Linq;
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
            string result = "[";
            for(int i=0; i<Goods.Count;i++)
            {
                if (i > 0) result += ",";
                result += Goods[i];
            }
            result += "]";
            return result;
        }
    }
}
