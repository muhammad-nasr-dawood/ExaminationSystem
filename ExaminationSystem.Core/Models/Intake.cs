using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Intake
{
    public int Id { get; set; }

    public byte IsRunning { get; set; }

    public virtual ICollection<IntakeDeptCourse> IntakeDeptCourses { get; set; } = new List<IntakeDeptCourse>();

    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();
}
