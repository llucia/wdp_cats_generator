using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WDP.Preprocessing
{
    public class FGAGenerator
    {
        public static void GenerateGraph(List<Bin> bins, string output)
        {
            var currentPath = "current.txt";
            var nextPath = "next.txt";
            string line;
            CompositeBid bidTmp;
            List<CompositeBid> newBids;
            bool newLine = false;
            var currentBin = new CompositeBin(bins[0]);
            using (TextWriter writer = new StreamWriter(currentPath))
            {
                foreach (var bid in currentBin.Bids)
                    writer.WriteLine(JsonConvert.SerializeObject(bid));
            }

            using (StreamWriter tw = new StreamWriter(output))
            {
                int i = 1;
                while (i < bins.Count)
                {
                    using (StreamWriter tempWriter = new StreamWriter(nextPath))
                    {
                        using (TextReader reader = File.OpenText(currentPath))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                bidTmp = JsonConvert.DeserializeObject<CompositeBid>(line);
                                newBids = bins[i].Bids.Select(b => bidTmp.Concat(b)).ToList();
                                if (newLine)tw.Write(tw.NewLine);else newLine = true;
                                tw.Write("[{0},{1},[{2}]]", bidTmp, bidTmp.Value, Print(newBids));
                                tw.Flush();
                                foreach (var compositeBid in newBids)
                                 tempWriter.WriteLine(JsonConvert.SerializeObject(compositeBid));   

                            }
                        }
                        File.Create(currentPath).Close();
                        var temp = currentPath;
                        currentPath = nextPath;
                        nextPath = temp;
                        i++;
                    }
                }
            }
            //using (TextWriter tw = File.CreateText(output))
            //{
            //    var currentBin =new CompositeBin(bins[0]);
            //    int i = 1;
            //    while (i < bins.Count)
            //    {
            //        var nextBin = bins[i];
            //        var tmpBin = new CompositeBin();
            //        foreach (var bid in currentBin.Bids)
            //        {
            //            var newBids = nextBin.Bids.Select(b => bid.Concat(b)).ToList();
            //            tw.Write(string.Format("[{0},{1},[{2}]]",bid,bid.Value,Print(newBids)));
            //            tmpBin.Bids.AddRange(newBids);
            //            if (i != bins.Count - 1 || bid !=currentBin.Bids.Last())
            //                tw.Write(tw.NewLine);
            //        }
                    
            //        i++;
            //        currentBin = tmpBin;
            //    }
            //}
        }

        private static object Print(List<CompositeBid> newBids)
        {
            StringBuilder sb = new StringBuilder();
            int count = newBids.Count();
            for (int i = 0; i <count ; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.Append(newBids[i]);
                
            }
           return sb.ToString();
        }

    }
}
