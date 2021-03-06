﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class CreditOrder : IOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int CreditForOrderId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
        public bool Paid { get; set; }
        public DateTime? DateEdited { get; set; }

        //FK
        public string ApplicationUserId { get; set; }
        public string AdminId { get; set; }

        //Navigational properties
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationUser Admin { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}