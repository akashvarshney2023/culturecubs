using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Culturecubs.Microsite.DataAccess.Models
{
    [Table("contest_tab")]
    public class ContestTab
    {
        [Key]
        public int Id { get; set; }
        public string? Key { get; set; }   // tab-name
        public string? Value { get; set; } // tab-html
        public int ContestId { get; set; }
    }
}
