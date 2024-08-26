using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Combi
    {
        [Key]
        public int CombiID { get; set; }
        public int PageID { get; set; }
        public string CombiCode { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DisplayPrice { get; set; }

        [JsonIgnore]
        public virtual MenuPage? MenuPage { get; set; }

        [JsonIgnore]
        public virtual ICollection<Item>? Items { get; set; }
    }
}
