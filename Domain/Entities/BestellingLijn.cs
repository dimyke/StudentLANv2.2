using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    class BestellingLijn
    {
        public int BestellingLijnId { get; set; }

        public int Aantal { get; set; }

        public int ConsumptieId { get; set; }

        public int BestellingId { get; set; }


    }
}
