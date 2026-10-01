using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.others
{
    [Table("ActivityLog")]
    public class ActivityLogModel
    {
        [Key]
        public int Id { get; set; }
    }
}
