using System;


namespace Domain.Entities
{
    public interface IOrder
    {
        int OrderId { get; set; }
        int UserId { get; set; }
        double TotalAmount { get; set; }
        DateTime date { get; set; }
    }
}
