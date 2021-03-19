using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DDACAssignment.Models
{
    public class DispatchingEntity : TableEntity
    {
        public DispatchingEntity(string dispatchingid, string orderid)
        {
            this.PartitionKey = dispatchingid;
            this.RowKey = orderid;
        }

        public DispatchingEntity() { }

        public string Status { get; set; }

    }
}
