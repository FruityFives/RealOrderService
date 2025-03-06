using Model;
using System.Collections.Generic;

namespace OrderService.Models
{
    
    public class Order
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
