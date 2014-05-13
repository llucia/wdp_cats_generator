using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class DGAGenerator
    {
        public static void GenerateGraph(List<Bin> bins)
        {
            using (TextWriter tw = File.CreateText("dga_output.txt"))
            {
                int i = 0;
                while (i<bins.Count-1)
                {
                    var currentBin = bins[i];
                    var nextBin = bins[i+1];
                    for (int j=0;j<currentBin.Bids.Count;j++)
                    {
                        var bid = currentBin.Bids[j];
                        string line = string.Format("[[{0}],{1},{2}]", bid,bid.Value.ToString(CultureInfo.InvariantCulture), nextBin);
                        tw.Write(line);
                        if (i != bins.Count - 2 || j< currentBin.Bids.Count-1)
                            tw.Write(tw.NewLine);
                    }
                    i++;
                }
            }
        }

    }
}
