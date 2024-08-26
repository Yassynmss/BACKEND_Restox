using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{

        public class DeliveryStatus
        {
            [Key]
            public int DeliveryStatusID { get; set; }

            [Required]
            [MaxLength(100)]
            public string DisplayDesc { get; set; }

        [JsonIgnore]

        public virtual ICollection<Order>? Orders { get; set; }
        }

    }
