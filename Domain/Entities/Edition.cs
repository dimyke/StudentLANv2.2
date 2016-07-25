using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //TODO: add # of sold?
    public class Edition
    {
        public int EditionId { get; set; }
        public string Location { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
