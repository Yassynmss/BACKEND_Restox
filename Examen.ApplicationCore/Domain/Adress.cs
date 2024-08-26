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

        // Clé étrangère pour BizAccount
        public int BizAccountID { get; set; }

        [ForeignKey("BizAccountID")]
        //[JsonIgnore]
        public virtual BizAccount? BizAccount { get; set; }

        [JsonIgnore]
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
