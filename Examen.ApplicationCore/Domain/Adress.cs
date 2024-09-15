using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Adress
    {
        [Key]
        public int AdressID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Ville { get; set; }


        public string? ApplicationUserId { get; set; } 

        [ForeignKey("ApplicationUserId")]
        [JsonIgnore]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [JsonIgnore]
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
