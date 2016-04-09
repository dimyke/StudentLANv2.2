using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public interface IOrder
    {
        [Key]
        int OrderId { get; set; }
        DateTime Date { get; set; }
        double TotalAmount { get; set; }
        bool Completed { get; set; }
        bool Deleted { get; set; }
        bool Paid { get; set; }
        DateTime? DateEdited { get; set; }

        //FK
        string ApplicationUserId { get; set; }

        //navigational properties
        //ICollection<Payment> Payments { get; set; }
    }
}
