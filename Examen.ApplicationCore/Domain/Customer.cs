using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime DatCrea { get; set; }
        public DateTime DatUpt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }
        public DateTime LastConnection { get; set; }
        public string Phone { get; set; }
        public int AdressID { get; set; }
        [JsonIgnore]

        public virtual Adress? Adress { get; set; }
        [JsonIgnore]

        public virtual ICollection<Order>? Orders { get; set; }
        [JsonIgnore]

        public virtual ICollection<CustomerReview>? CustomerReviews { get; set; }
    }
}
