using System;


namespace Domain.Entities
{
    public interface IOrder
    {
        int OrderId { get; set; }
        DateTime Date { get; set; }
        double TotalAmount { get; set; }
        bool Completed { get; set; }
        bool Deleted { get; set; }
        bool Paid { get; set; }
        DateTime? DateEdited { get; set; }

        //FK
        string ApplicationUserId { get; set; }
    }
}
