using System;
using System.Collections.Generic;

namespace HSH_Db_Assignment3.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseLists = new HashSet<CourseList>();
            GradeLists = new HashSet<GradeList>();
        }

        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;

        public virtual ICollection<CourseList> CourseLists { get; set; }
        public virtual ICollection<GradeList> GradeLists { get; set; }
    }
}
