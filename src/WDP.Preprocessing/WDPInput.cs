using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDP.Preprocessing
{
    public class WDPInput
    {
        public int NumberOfGoods { get; set; }
        public int NumberOfBids { get; set; }
        public int NumberOfDummy { get; set; }

        public List<Bid> Bids { get; set; }
    }
}
