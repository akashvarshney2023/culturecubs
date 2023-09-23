using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContentManagement.DTO
{

    public class ContentMnagementDTO
    {       
        

        [JsonProperty(PropertyName = "tenantId")]
        public int TenantId { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }


        [JsonProperty(PropertyName = "microsite")]
        public MicrositeDTO microsite { get; set; }

    }

    
    public class MicrositeDTO
    {           

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "description")]

        public string description { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public List<PageDTO> Pages { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }
    }

   
    public class PageDTO
    {
        

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "key")]

        public string Key { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "dateCreated")]
        public DateTime DateCreated { get; set; }

        //  public Microsite microsites { get; set; }


    }


}
