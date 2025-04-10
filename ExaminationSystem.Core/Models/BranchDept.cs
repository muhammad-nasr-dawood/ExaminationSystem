using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class BranchDept
{
    public int BranchId { get; set; }

    public int DeptId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Department Dept { get; set; } = null!;

    public virtual StaffBranchDepartmentManagement? StaffBranchDepartmentManagement { get; set; }

    public virtual ICollection<StaffBranchDepartmentWorksFor> StaffBranchDepartmentWorksFors { get; set; } = new List<StaffBranchDepartmentWorksFor>();

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();
}
