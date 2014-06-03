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
            var seed = wdpInput.NumberOfGoods + wdpInput.NumberOfDummy;
            var dummyGoods = Enumerable.Range(seed, bins.Count).ToList();
            int index = wdpInput.NumberOfBids;
            for (int i = 0; i < dummyGoods.Count; i++)
            {
                bins[i].Bids.Add(new Bid() {Id=index++ , Goods = new List<long>() {dummyGoods[i]}});
            }
            wdpInput.NumberOfDummy += dummyGoods.Count;
            bins.Add(new Bin(){Bids=new List<Bid>()});

            
            return bins;
        }

        public class GoodScore
        {
            public int Good { get; set; }
            public int Score { get; set; }

        }

        public static List<Bin> GetBinsHeuristic(WDPInput wdpInput)
        {
            var goods = Enumerable.Range(0, wdpInput.NumberOfGoods).ToList();
            var bids = wdpInput.Bids.Select(b => b).ToList();
            var bins = new List<Bin>();
            while (goods.Count>0)
            {
                var scores = goods.Select(
                    g => new GoodScore {Good = g, Score = bids.Count(b => b.Goods.Contains(g))}).ToList();
                var maxGood =scores.First(score=>score.Score== scores.Max(s=>s.Score)).Good;
                bins.Add(new Bin()
                {
                    Bids = bids.Where(bid => bid.Goods.Contains(maxGood)).ToList()
                });
                bids.RemoveAll(bid => bid.Goods.Contains(maxGood));
                goods.Remove(maxGood);

            }


        //var bins = goods.Select(good => new Bin()
            //{
            //    Bids = wdpInput.Bids.Where(bid => bid.MinimumGood() == good).ToList()
            //}).ToList();

            bins.RemoveAll(bin => !bin.Bids.Any());
            var seed = wdpInput.NumberOfGoods + wdpInput.NumberOfDummy;
            var dummyGoods = Enumerable.Range(seed, bins.Count).ToList();
            int index = wdpInput.NumberOfBids;
            for (int i = 0; i < dummyGoods.Count; i++)
            {
                bins[i].Bids.Add(new Bid() { Id = index++, Goods = new List<long>() { dummyGoods[i] } });
            }
            wdpInput.NumberOfDummy += dummyGoods.Count;
            bins.Add(new Bin() { Bids = new List<Bid>() });


            return bins;
        }
    }
}
