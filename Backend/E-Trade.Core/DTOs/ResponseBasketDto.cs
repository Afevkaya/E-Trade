using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.DTOs
{
    public class ResponseBasketDto
    {
        public string ProductName { get; set; }
        public string AppUserName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Total { get; set; }

    }
}
