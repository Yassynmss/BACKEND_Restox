using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class CustomerReview
    {
        [Key]
        public int CustomerReviewID { get; set; }
        public int CustomerID { get; set; }
        public int OrderID { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public bool IsVerified { get; set; }
        public DateTime DatCrea { get; set; }
        public DateTime DatUpt { get; set; }
        [JsonIgnore]

        public virtual Order? Order { get; set; }
        [JsonIgnore]

        public virtual Customer? Customer { get; set; }
    }
}
