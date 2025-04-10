using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class IntakeDeptCourse
{
    public int DeptId { get; set; }

    public int CourseId { get; set; }

    public int IntakeId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Department Dept { get; set; } = null!;

    public virtual Intake Intake { get; set; } = null!;

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();
}
