using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationDevelopment.Entities
{
    [Table("Course")]
    public partial class Course
    {
        public int CourseId { get; set; }

        [StringLength(60)]
        public string CourseName { get; set; }

        public int? CourseCateId { get; set; }

        [StringLength(200)]
        public string CourseDescription { get; set; }

        public virtual AssignedCourse AssignedCourse { get; set; }

        public virtual CourseCategory CourseCategory { get; set; }
    }
}
