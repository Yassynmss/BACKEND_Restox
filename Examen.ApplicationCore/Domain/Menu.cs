using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }

        public string Title { get; set; }
        public string HtmlDescription { get; set; }

        public string? ApplicationUserID { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [JsonIgnore]
        public virtual ICollection<MenuPage>? MenuPages { get; set; }
    }
}
