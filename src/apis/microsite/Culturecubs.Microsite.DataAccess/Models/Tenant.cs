using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.Microsite.DataAccess.Models
{
    [Table("tenant")]
    public class Tenant
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? URL { get; set; }
        public string? EmailId { get; set; }
        public string? Contact { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string? Code { get; set; }

    }
}
