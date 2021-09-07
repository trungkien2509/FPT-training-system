namespace FPT_Training_System.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            AssignedCourses = new HashSet<AssignedCourse>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Fullname { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? Age { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [StringLength(50)]
        public string Education { get; set; }

        [StringLength(50)]
        public string MainPpLanguage { get; set; }

        public int? ToeicScore { get; set; }

        [StringLength(100)]
        public string ExperienceDetails { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(60)]
        public string Location { get; set; }

        [StringLength(60)]
        public string Contact { get; set; }

        [StringLength(15)]
        public string isActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignedCourse> AssignedCourses { get; set; }
    }
}
