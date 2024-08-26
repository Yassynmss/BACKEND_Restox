using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        public string ShortDescription { get; set; }
        public string ISOCode { get; set; }
        public string DisplayCode { get; set; }
        [JsonIgnore]

        public virtual ICollection<ItemDetail>? ItemDetails { get; set; }
    }

}
