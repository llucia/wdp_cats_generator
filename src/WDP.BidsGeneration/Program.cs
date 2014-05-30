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
            var wdpInput =  BidsParser.Parse(input);
            var bins = BaseGenerator.GetBins(wdpInput);
            Console.WriteLine("Choose an algorithm: 0-FGA, 1-DGA, 2-DMA encoded, 3-DMA long");
            var algorithm = Console.ReadLine();
            Console.WriteLine(startDate+"Starting..");
            
            
            switch (algorithm)
            {
                case "0":
                    Console.WriteLine(DateTime.Now + " Generating FGA file");
                    FGAGenerator.GenerateGraph(bins,
                        output + "fga_" + wdpInput.NumberOfGoods + "_" + wdpInput.NumberOfBids + "_");
                    break;
                case "1":
                    Console.WriteLine(DateTime.Now + " Generating DGA file");
                    DGAGenerator.GenerateGraph(bins,
                        output + "dga_" + wdpInput.NumberOfGoods + "_" + wdpInput.NumberOfBids + "_");
                    break;
                case "2":
                     Console.WriteLine(DateTime.Now + " Generating DMA Encoded file");
                    DMAEncodedGenerator.GenerateGraph(bins,wdpInput.NumberOfGoods,wdpInput.NumberOfDummy,
                        output + "dma_encoded_" + wdpInput.NumberOfGoods + "_" + wdpInput.NumberOfBids + "_");
                    break;
                case "3":
                    Console.WriteLine(DateTime.Now + " Generating DMA Long file");
                    DMAGenerator.GenerateGraph(bins,
                        output + "dma_long" + wdpInput.NumberOfGoods + "_" + wdpInput.NumberOfBids + "_");
                    break;
                case "x":
                    
                    Console.WriteLine("type encoded number");
                    var n = Console.ReadLine();
                    PrintNumbers(n);
                    break;
            }
            var endDate = DateTime.Now;
            Console.WriteLine(endDate+ " END!");
            Console.WriteLine("Total time " + (endDate-startDate));
            Console.ReadLine();

        }

        private static void PrintNumbers(string n)
        {
            List<long> result = new List<long>();
            var list = n.Split(',');
            int count = list.Length-1;
            for (int i = 0; i < list.Length; i++)
            {
                long shift = (count - i)*64;
                var encoded=Convert.ToString(long.Parse(list[i]), 2);
                for (int j = 0; j < encoded.Length; j++)
                {
                    if(encoded[encoded.Length-1-j]=='1')result.Add(j+shift);
                }

            }
            var s = string.Join(",", result);
            Console.WriteLine(s);
        }
    }
}
