using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationDevelopment.Entities
{
    [Table("AssignedCourse")]
    public partial class AssignedCourse
    {
        [Key]
        public int AssignedId { get; set; }

        public int? UserId { get; set; }

        public int? CourseId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Course Course { get; set; }
    }
}
