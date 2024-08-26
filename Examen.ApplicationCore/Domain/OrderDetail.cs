using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public decimal Price { get; set; }
        public string DeliveryStatus { get; set; }
        [JsonIgnore]

        public virtual Order? Order { get; set; }
        [JsonIgnore]
            
        public virtual Item? Item { get; set; }
    }
}
