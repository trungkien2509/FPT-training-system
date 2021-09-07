namespace FPT_Training_System.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
