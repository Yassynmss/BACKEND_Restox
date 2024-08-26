using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examen.ApplicationCore.Domain
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }

        public string Title { get; set; }
        public string HtmlDescription { get; set; }

        // Clé étrangère pointant vers BizAccount
        public int BizAccountID { get; set; }

        // Propriété de navigation pour la relation avec BizAccount
        [JsonIgnore]
        public virtual BizAccount? BizAccount { get; set; }

        [JsonIgnore]
        public virtual ICollection<MenuPage>? MenuPages { get; set; }
    }
}
