using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class BaseGenerator
    {
        public static List<Bin> GetBins(WDPInput wdpInput)
        {
            var goods = Enumerable.Range(0, wdpInput.NumberOfGoods);
            var bins = goods.Select(good => new Bin()
            {
                Bids = wdpInput.Bids.Where(bid => bid.MinimumGood() == good).ToList()
            }).ToList();
            bins.RemoveAll(bin => !bin.Bids.Any());
            int index = 0;
            var seed = wdpInput.NumberOfGoods + wdpInput.NumberOfDummy;
            var dummyGoods = Enumerable.Range(seed, bins.Count).ToList();
            for (int i = 0; i < dummyGoods.Count; i++)
            {
                bins[i].Bids.Add(new Bid() {Goods = new List<long>() {dummyGoods[i]}});
            }
            wdpInput.NumberOfDummy += dummyGoods.Count;
            bins.Add(new Bin(){Bids=new List<Bid>()});
            bins.ForEach(bin => bin.Bids.ForEach(bid => bid.Id = index++));

            
            return bins;
        }
    }
}
