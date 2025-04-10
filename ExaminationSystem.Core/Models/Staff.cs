using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Staff
{
    public long Ssn { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual User SsnNavigation { get; set; } = null!;

    public virtual StaffBranchDepartmentManagement? StaffBranchDepartmentManagement { get; set; }

    public virtual ICollection<StaffBranchDepartmentWorksFor> StaffBranchDepartmentWorksFors { get; set; } = new List<StaffBranchDepartmentWorksFor>();

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    public virtual StaffBranchManage? StaffBranchManage { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
