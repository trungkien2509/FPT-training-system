namespace FPT_Training_System.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            AssignedCourses = new HashSet<AssignedCourse>();
        }

        public int CourseId { get; set; }

        [StringLength(60)]
        public string CourseName { get; set; }

        public int? CourseCateId { get; set; }

        [StringLength(200)]
        public string CourseDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedCourse> AssignedCourses { get; set; }

        public virtual CourseCategory CourseCategory { get; set; }
    }
}
