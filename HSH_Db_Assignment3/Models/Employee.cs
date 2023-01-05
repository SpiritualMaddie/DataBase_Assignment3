using System;
using System.Collections.Generic;

namespace HSH_Db_Assignment3.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ClassLists = new HashSet<ClassList>();
            ContactInfos = new HashSet<ContactInfo>();
            CourseLists = new HashSet<CourseList>();
            GradeLists = new HashSet<GradeList>();
        }

        public int EmployeeId { get; set; }
        public string SocialSecurityNr { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Titel { get; set; } = null!;

        public virtual ICollection<ClassList> ClassLists { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
        public virtual ICollection<CourseList> CourseLists { get; set; }
        public virtual ICollection<GradeList> GradeLists { get; set; }
    }
}
