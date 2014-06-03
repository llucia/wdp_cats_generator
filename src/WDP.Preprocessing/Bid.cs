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
        public long[] EncodedGoods { get; set; }
        public long PartitionId { get; set; }

        public long Id { get; set; }
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

        public string Encoded()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < EncodedGoods.Length; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append(EncodedGoods[i]);
            }
            sb.Append("]");
            return sb.ToString();
            
        }

        public string LongEncoded()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(PartitionId);
            for (int i = 0; i < EncodedGoods.Length; i++)
            {
                sb.Append(",");
                sb.Append(EncodedGoods[i]);
            }
            sb.Append("]");
            return sb.ToString();

        }

        internal void EncodeBid(int m)
        {
            EncodedGoods=new long[m/64+1];
            int n = m/64 ;
            long exp, index;
            foreach (var good in Goods)
            {
                exp = good%64;
                index = good/64;
                EncodedGoods[n - index] |= ((long)Math.Pow(2, exp));
            }
        }
    }
}
