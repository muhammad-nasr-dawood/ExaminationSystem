using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class StaffBranchIntakeDepartmentCourseTeach
{
    public long StaffSsn { get; set; }

    public int BranchId { get; set; }

    public int DepartmentId { get; set; }

    public int CourseId { get; set; }

    public int IntakeId { get; set; }

    public DateTime StartingDate { get; set; }

    public DateTime? EndingDate { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual BranchDept BranchDept { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual Intake Intake { get; set; } = null!;

    public virtual IntakeDeptCourse IntakeDeptCourse { get; set; } = null!;

    public virtual Staff StaffSsnNavigation { get; set; } = null!;
}
