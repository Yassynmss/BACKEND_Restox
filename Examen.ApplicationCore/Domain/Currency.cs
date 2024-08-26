using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Currency
    {
        [Key]
        public int CurrencyID { get; set; }
        public string ShortDescription { get; set; }
        public string MoneyCode { get; set; }
        [JsonIgnore]
        public virtual ICollection<ItemPrice>? ItemPrices { get; set; }
    }
}
