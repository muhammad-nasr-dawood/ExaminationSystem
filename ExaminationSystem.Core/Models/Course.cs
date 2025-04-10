using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Duration { get; set; }

    public virtual ICollection<IntakeDeptCourse> IntakeDeptCourses { get; set; } = new List<IntakeDeptCourse>();

    public virtual ICollection<Pool> Pools { get; set; } = new List<Pool>();

    public virtual ICollection<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; } = new List<StaffBranchIntakeDepartmentCourseTeach>();

    public virtual ICollection<Topic> Tops { get; set; } = new List<Topic>();
}
