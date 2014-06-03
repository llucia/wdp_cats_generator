using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class BidsParser
    {
        public static WDPInput Parse(string path)
        {
            var wdpInput = new WDPInput() { Bids = new List<Bid>() };
            //return new WDPInput()
            //{
            //    Bids = new List<Bid>()
            //    {
            //        new Bid {Goods = {1,2}, Value = 10},
            //        new Bid {Goods = {1,3}, Value = 50},
            //        new Bid {Goods = {3,4}, Value = 20},
            //        new Bid {Goods = {3,5}, Value = 30},
            //        new Bid {Goods = {5,6}, Value = 40},

            //    },
            //    NumberOfGoods = 6,
            //    NumberOfBids = 5,
            //    NumberOfDummy = 0
            //};

            using (TextReader rdr = new StreamReader(path))
            {
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    if (line.StartsWith("%") || string.IsNullOrEmpty(line)) continue;
                    var split = line.Split(' ', '\t');
                    var label = split[0];
                    switch (label)
                    {
                        case "goods": wdpInput.NumberOfGoods = int.Parse(split[1]); continue;
                        case "bids": wdpInput.NumberOfBids = int.Parse(split[1]); continue;
                        case "dummy": wdpInput.NumberOfDummy = int.Parse(split[1]); continue;
                    }
                    try
                    {
                        var bid = new Bid {Id =long.Parse(split[0]),  Value = double.Parse(split[1], CultureInfo.InvariantCulture), Goods = new List<long>() };
                        for (var i = 2; i < split.Length - 1; i++)
                            bid.Goods.Add(long.Parse(split[i]));
                        wdpInput.Bids.Add(bid);
                    }
                    catch
                    {
                        Console.WriteLine("Error parsing line " + line);
                    }
                }
            }

            return wdpInput;
        }
    }
}
