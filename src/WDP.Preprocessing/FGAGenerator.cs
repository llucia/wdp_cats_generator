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
            var chunkSize = 200000000;//200MB
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
            int chunk = 1;
            StreamWriter tw;
            tw = new StreamWriter(output + chunk + ".txt");
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
                            if (newLine) tw.Write(tw.NewLine);
                            else newLine = true;
                            tw.Write("[{0},{1},[{2}]]", bidTmp, bidTmp.Value, Print(newBids));
                            foreach (var compositeBid in newBids)
                                tempWriter.WriteLine(JsonConvert.SerializeObject(compositeBid));

                            if (tw.BaseStream.Length > chunkSize)
                            {
                                tw.Flush();
                                tw.Close();
                                chunk++;
                                tw = new StreamWriter(output + chunk + ".txt");
                                newLine = false;
                            }
                        }
                        
                    }
                    File.Create(currentPath).Close();
                    var temp = currentPath;
                    currentPath = nextPath;
                    nextPath = temp;
                    i++;
                }
            }
            tw.Flush();
            tw.Close();
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
