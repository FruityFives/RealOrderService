using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderItem
    {
        public int ProductId { get; set; } // Produktets ID - bliver ikke brugt
        public string ProductName { get; set; } = string.Empty; // Navn på produktet
        public int Quantity { get; set; } // Antal af produktet
       
    }
}
