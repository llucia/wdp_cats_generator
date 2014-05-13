using System.Collections.Generic;

namespace WDP.Preprocessing
{
    public class CompositeBid
    {
        public CompositeBid()
        {
            Goods=new List<List<long>>();
        }
        public CompositeBid(Bid bid)
        {
            Goods = new List<List<long>>(){bid.Goods};
            Value = bid.Value;
        }
        public double Value { get; set; }
        public List<List<long>> Goods { get; set; }

        public CompositeBid Concat(Bid bid)
        {
            var goods = new List<List<long>>();
            foreach (var good in Goods)
                goods.Add(good);
            goods.Add(bid.Goods);
            return new CompositeBid {Goods = goods, Value=bid.Value};
        }
        public override string ToString()
        {

            string result = "[";
            for (int j = 0; j < Goods.Count; j++)
            {
                if (j > 0) result += ",";

                result += "[";
                for (int i = 0; i < Goods[j].Count; i++)
                {
                    if (i > 0) result += ",";
                    result += Goods[j][i];
                }
                result += "]";
            }
            
            result += "]";

            return result;
        }
    }
}