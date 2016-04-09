using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentSort Type { get; set; }
        // foreign key
        public int OrderID { get; set; }
        public string ApplicationUserId { get; set; }

        //navigational properties
        public KitchenOrder KitchenOrder { get; set; }
    }
}
