using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Organization { get; set; }
        public DateTime DatCrea { get; set; }
        public DateTime DatUpt { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLocked { get; set; }

        public string? Photo { get; set; } // Nouvelle propriété pour stocker le chemin de la photo

      //  [JsonIgnore]
        public virtual ICollection<Adress>? Adresses { get; set; } = new List<Adress>();

        [JsonIgnore]
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();

        public int? RoleId { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }

}
