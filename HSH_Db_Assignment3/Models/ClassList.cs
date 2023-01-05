using System;
using System.Collections.Generic;

namespace HSH_Db_Assignment3.Models
{
    public partial class ClassList
    {
        public int ClassListId { get; set; }
        public string FkClassCode { get; set; } = null!;
        public int FkStudentId { get; set; }
        public int FkEmployeeId { get; set; }

        public virtual Class FkClassCodeNavigation { get; set; } = null!;
        public virtual Employee FkEmployee { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
