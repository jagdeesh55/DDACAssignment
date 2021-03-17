using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDACAssignment.Models
{
    public class Item
    {
        public string ID { get; set; }
        public string ItemName { get; set; }
        public DateTime ItemEntered { get; set; }
        public int Quantity { get; set; }
    }
}
