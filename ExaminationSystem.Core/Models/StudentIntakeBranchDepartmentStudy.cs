using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class StudentIntakeBranchDepartmentStudy
{
    public long StudentSsn { get; set; }

    public int BranchId { get; set; }

    public int DepartmentId { get; set; }

    public int IntakeId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual BranchDept BranchDept { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual Intake Intake { get; set; } = null!;

    public virtual Student StudentSsnNavigation { get; set; } = null!;
}
