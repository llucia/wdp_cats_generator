using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class DMAGenerator
    {
        public static void GenerateGraph(List<Bin> bins,string output)
        {
            var chunkSize = 200000000;//200MB
            var index = 0;
            int chunk = 1;
            StreamWriter tw;
            bool newLine = false;
            tw = new StreamWriter(output + chunk + ".txt");
                int i = 0;
                while (i<bins.Count-1)
                {
                    var currentBin = bins[i];
                    var nextBin = bins[i+1];
                    for (int j=0;j<currentBin.Bids.Count;j++)
                    {
                        var bid = currentBin.Bids[j];
                        string line = string.Format("[{0},{1},{2},{3}]",index++, bid,bid.Value.ToString(CultureInfo.InvariantCulture), nextBin.ToDMAString());
                        if (newLine) tw.Write(tw.NewLine);
                        else newLine = true;
                        tw.Write(line);
                        if (tw.BaseStream.Length > chunkSize)
                        {
                            tw.Flush();
                            tw.Close();
                            chunk++;
                            tw = new StreamWriter(output + chunk + ".txt");
                            newLine = false;
                        }
                    }
                    i++;
                }

                tw.Flush();
                tw.Close();
        
        }

    }
}
