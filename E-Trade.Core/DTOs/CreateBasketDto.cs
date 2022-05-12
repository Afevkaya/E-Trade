using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Trade.Core.DTOs
{
    public class CreateBasketDto
    {
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }

    }
}
