using Model;
using System.Collections.Generic;

namespace OrderService.Models
{
    public class Order
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int CompanyId { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public string Status { get; set; }
    }
}
