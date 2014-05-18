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
            string path = "C:/Users/liannetr/Desktop/wdp cats/";
            Console.WriteLine("Enter input file");
            string input=path+Console.ReadLine();
            Console.WriteLine("Enter output path");
            string output=path+Console.ReadLine();
            var startDate = DateTime.Now;
            Console.WriteLine(startDate+"Starting..");
            var wdpInput =  BidsParser.Parse(input);
            var bins = BaseGenerator.GetBins(wdpInput);
            Console.WriteLine("Generating DGA file");
           // DGAGenerator.GenerateGraph(bins,output+"dga_"+wdpInput.NumberOfGoods+"_"+wdpInput.NumberOfBids+".txt");
            FGAGenerator.GenerateGraph(bins, output + "fga_" + wdpInput.NumberOfGoods + "_" + wdpInput.NumberOfBids + ".txt");
            var endDate = DateTime.Now;
            Console.WriteLine(endDate+ " END!");
            Console.WriteLine("Total time " + (endDate-startDate));
            Console.ReadLine();

        }
    }
}
