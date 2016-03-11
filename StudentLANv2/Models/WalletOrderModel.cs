using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace StudentLANv2.Models
{
    public class WalletOrderModel
    {
        public WalletOrder WalletOrder { get; set; }
        public ApplicationUser User { get; set; }

    }
}