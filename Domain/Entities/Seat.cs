using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int SeatRow { get; set; }
        public int SeatColumn { get; set; }
        public Seatsort Sort { get; set; }
        public enum State { Booked,Reserved,Free }

        //fk
        public int EditionId { get; set; }

        // navigiational
        public Edition edition { get; set; }
    }
}
