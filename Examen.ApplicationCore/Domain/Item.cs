using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Item
    {
        [Key]
        public int itemID { get; set; }

        public string ShortDescription { get; set; }

        public int ItemOrder { get; set; }

        public string AnimationUrl { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemType Name { get; set; }

        public int PageID { get; set; }

        public int CombiID { get; set; }

        // Suppression de l'attribut JsonIgnore pour MenuPage si vous souhaitez l'inclure dans la sérialisation JSON
        [JsonIgnore]
        public virtual MenuPage? MenuPage { get; set; }

        [JsonIgnore]
        public virtual Combi? Combi { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemDetail>? ItemDetails { get; set; }

        [ForeignKey("ItemPrice")] 
        public int ItemPriceID { get; set; }

        [JsonIgnore]
        public virtual ICollection<ItemPrice>? ItemPrices { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

        [JsonIgnore]
        public virtual CustomerReview? CustomerReview { get; set; }
    }
}
