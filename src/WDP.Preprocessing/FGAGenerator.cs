using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class FGAGenerator
    {
        public static void GenerateGraph(List<Bin> bins)
        {
            using (TextWriter tw = File.CreateText("fga_output.txt"))
            {
                var currentBin =new CompositeBin(bins[0]);
                int i = 1;
                while (i < bins.Count)
                {
                    var nextBin = bins[i];
                    var tmpBin = new CompositeBin();
                    foreach (var bid in currentBin.Bids)
                    {
                        var newBids = nextBin.Bids.Select(b => bid.Concat(b)).ToList();
                        tw.Write(string.Format("[{0},{1},[{2}]]",bid,bid.Value,Print(newBids)));
                        tmpBin.Bids.AddRange(newBids);
                        if (i != bins.Count - 1 || bid !=currentBin.Bids.Last())
                            tw.Write(tw.NewLine);
                    }
                    
                    i++;
                    currentBin = tmpBin;
                }
            }
        }

        private static object Print(List<CompositeBid> newBids)
        {
            var result = "";
            newBids.ForEach(bid=>result+=bid+",");
            if(result.Length>1)
                result=result.Remove(result.Length - 1, 1);
            return result;
        }

    }
}
