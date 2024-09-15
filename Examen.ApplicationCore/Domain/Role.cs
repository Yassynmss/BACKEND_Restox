using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public RoleType Name { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser? User { get; set; }
    }
}
