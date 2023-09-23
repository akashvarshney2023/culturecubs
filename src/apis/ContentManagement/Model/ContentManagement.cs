using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Model
{
    [Table("ContentManagement")]
    public class ContentManagement
    {
        [Key]
        [JsonProperty(PropertyName = "contentId")]
        public int ContentId { get; set; }

        [JsonProperty(PropertyName = "tenantId")]
        public int TenantId { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

        //[JsonProperty(PropertyName = "microsite")]
        //public Microsite microsite { get; set; }
 
    }

    [Table("Microsite")]
    public class Microsite
    {
        [Key]
        [JsonProperty(PropertyName = "MicrositeId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MicrositeId { get; set; }

        [JsonProperty(PropertyName = "contentId")]
        public int ContentId { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "description")]

        public string description { get; set; }

        [JsonProperty(PropertyName = "pages")]

        public List<Page> Pages { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

       // public ContentManagement contentManagement { get; set; }

    }

    [Table("Page")]
    public class Page
    {
        [Key]
        [JsonProperty(PropertyName = "pageId")]
        public int PageId { get; set; }


        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "key")]

        public string Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }        

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }
        [JsonProperty(PropertyName = "micrositeId")]
        public int MicrositeId { get; set; }
        //[JsonProperty(PropertyName = "microsite")]
        //public Microsite microsite { get; set; }



    }


}
