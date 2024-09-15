using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public int DeliveryStatusID { get; set; }
        public int DeliveryTypeID { get; set; }
       // public DateTime DatCrea { get; set; }
      //  public DateTime DatUpt { get; set; }
       // [JsonIgnore]

       // public virtual Customer? Customer { get; set; }
        [JsonIgnore]

        public virtual ApplicationUser? ApplicationUser { get; set; }
        [JsonIgnore]

        public virtual DeliveryStatus? DeliveryStatus { get; set; }
        [JsonIgnore]

        public virtual DeliveryType? DeliveryType { get; set; }
        [JsonIgnore]

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        [JsonIgnore]

        public virtual CustomerReview? CustomerReview { get; set; }
    }
}
