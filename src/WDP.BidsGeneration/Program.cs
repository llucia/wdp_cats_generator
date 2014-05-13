using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDP.Preprocessing;

namespace WDP.BidsGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Program Files\CATS-windows\0000.txt";
            var wdpInput =  BidsParser.Parse(path);
            var bins = BaseGenerator.GetBins(wdpInput);
            DGAGenerator.GenerateGraph(bins);
            FGAGenerator.GenerateGraph(bins);

        }
    }
}
