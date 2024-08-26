using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class MenuPage
    {
        [Key]
        public int MenuPageID { get; set; }
        public string ShortDescription { get; set; }
        public string HtmlDescription { get; set; }
        public string PageOrder { get; set; }
        public string AnimationUrl { get; set; }
        public int MenuID { get; set; }
        [JsonIgnore]

        public virtual Menu? Menu { get; set; }
        [JsonIgnore]

        public virtual ICollection<Item>? Items { get; set; }
        [JsonIgnore]

        public virtual ICollection<Combi>? Combis { get; set; }
    }
}
