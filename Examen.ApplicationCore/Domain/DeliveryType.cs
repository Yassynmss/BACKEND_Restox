using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class DeliveryType
    {
        [Key]
        public int DeliveryTypeID { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayDesc { get; set; }

        [JsonIgnore]

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
