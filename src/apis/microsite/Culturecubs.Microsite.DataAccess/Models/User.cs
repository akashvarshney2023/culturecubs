using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culturecubs.Microsite.DataAccess.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Location { get; set; }
        public string UserSkillSet { get; set; }
        public string EmailId { get; set; }
        public string? Contact { get; set; }
        public long TenantId { get; set; }
        public int? RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
