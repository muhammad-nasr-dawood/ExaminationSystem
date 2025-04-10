using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Disc { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<BranchDept> BranchDepts { get; set; } = new List<BranchDept>();

    public virtual ICollection<IntakeDeptCourse> IntakeDeptCourses { get; set; } = new List<IntakeDeptCourse>();

    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    public virtual ICollection<StaffBranchDepartmentManagement> StaffBranchDepartmentManagements { get; set; } = new List<StaffBranchDepartmentManagement>();

    public virtual ICollection<StaffBranchDepartmentWorksFor> StaffBranchDepartmentWorksFors { get; set; } = new List<StaffBranchDepartmentWorksFor>();

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    public virtual ICollection<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; } = new List<StudentIntakeBranchDepartmentStudy>();
}
