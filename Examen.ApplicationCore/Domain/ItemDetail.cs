using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class ItemDetail
    {
        [Key]
        public int ItemDetailID { get; set; }
        public int itemID { get; set; }
        public int LanguageID { get; set; }
        public string Description { get; set; }
        public string HtmlDescription { get; set; }
        [JsonIgnore]

        public virtual Item? Item { get; set; }
        [JsonIgnore]

        public virtual Language? Language { get; set; }

    }
}
