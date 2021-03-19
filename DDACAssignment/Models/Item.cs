using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DDACAssignment.Models
{
    public class Item
    {
        [Display(Name = "Item ID")]
        public string ID { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Date Entered")]
        public DateTime ItemEntered { get; set; }
        [Display(Name = "Amount")]
        public int Quantity { get; set; }
    }
}
