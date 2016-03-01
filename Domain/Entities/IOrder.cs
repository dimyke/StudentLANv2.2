using System;


namespace Domain.Entities
{
    public interface IOrder
    {
        int OrderId { get; set; }
        int UserId { get; set; }
        double TotalAmount { get; set; }
        DateTime Date { get; set; }
        bool Completed { get; set; }
        bool Deleted { get; set; }
    }
}
