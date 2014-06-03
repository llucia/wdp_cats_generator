using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class DMALongEncodedGenerator
    {
        public static void GenerateGraph(List<Bin> bins,int numberOfGoods,int numberOfDummies,string output)
        {
            using (StreamWriter tempWriter = new StreamWriter(output+"profile.txt"))
            {

                tempWriter.WriteLine("************************Memory Bounds********************" );
                tempWriter.WriteLine("# goods: "+numberOfGoods);
                tempWriter.WriteLine("# dummies: " + numberOfDummies);
                tempWriter.WriteLine("# bins: " + (bins.Count-1));
                int total = 0;
                int ind = 0;
                int n1nk = 1;
                bins.ForEach(bin =>
                {
                    if (bin.Bids.Count > 0)
                    {
                        total += bin.Bids.Count;
                        n1nk *= bin.Bids.Count;
                        tempWriter.WriteLine("# bids n" + ind + ": " + bin.Bids.Count);
                    }
                    ind++;
                });
                tempWriter.WriteLine("# bids total: " + total);
                double totalMsgBytes = ((numberOfDummies + numberOfGoods)/8d + 24)*n1nk;
                double totalEdgesBytes = ((numberOfDummies + numberOfGoods) / 8d + 16) * n1nk;
                double totalVertexBytes = total*((numberOfGoods + numberOfDummies)/8d+16);
                tempWriter.WriteLine("# Vertex Bytes Required: " +totalVertexBytes );
                tempWriter.WriteLine("# Edges Bytes Required: " + totalEdgesBytes);
                tempWriter.WriteLine("# Messages Bytes Required: " + totalMsgBytes);
                tempWriter.WriteLine("# MB Required: " +(totalVertexBytes+totalMsgBytes+totalEdgesBytes)/(1024*1024d) );
            }

            int partitionId = 0;
            foreach (var bin in bins)
            {
                bin.EncodeBids(numberOfGoods+numberOfDummies);
                bin.Bids.ForEach(bid=>bid.PartitionId=partitionId++);
            }

            var chunkSize = 200000000;//200MB
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
                        string line = string.Format("[{0},{1},{2},{3}]",bid.PartitionId, bid.Encoded(),bid.Value.ToString(CultureInfo.InvariantCulture), nextBin.LongEncoded());
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
