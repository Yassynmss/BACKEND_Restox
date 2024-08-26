using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        public string ShortDescription { get; set; }
        public int ItemOrder { get; set; }
        public string AnimationUrl { get; set; }
        public int PageID { get; set; }
        public int CombiID { get; set; }
        [JsonIgnore]

        public virtual MenuPage? MenuPage { get; set; }
        [JsonIgnore]

        public virtual Combi? Combi { get; set; }
        [JsonIgnore]

        public virtual ICollection<ItemDetail>? ItemDetails { get; set; }
        [JsonIgnore]

        public virtual ICollection<ItemPrice>? ItemPrices { get; set; }
        [JsonIgnore]

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        [JsonIgnore]

        public virtual CustomerReview? CustomerReview { get; set; }
    }
}
