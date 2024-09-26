using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class BizAccount
    {
        [Key]
        public int BAccountID { get; set; }

        public string Pseudo { get; set; }
        public string Organization { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DatCrea { get; set; }
        public DateTime DatUpt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }

        // Relation avec Adress avec suppression en cascade
        public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

        // Propriété de navigation pour la relation avec Menu
        [JsonIgnore]
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
