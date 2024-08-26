using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class ItemPrice
    {
        [Key]
        public int ItemPriceID { get; set; }
        public int ItemID { get; set; }
        public int CurrencyID { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DisplayPrice { get; set; }
        [JsonIgnore]

        public virtual Item? Item { get; set; }
        [JsonIgnore]

        public virtual Currency? Currency { get; set; }
    }
}
