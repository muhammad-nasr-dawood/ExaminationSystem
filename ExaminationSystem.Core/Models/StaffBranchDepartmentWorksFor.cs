using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class StaffBranchDepartmentWorksFor
{
    public long StaffSsn { get; set; }

    public int BranchId { get; set; }

    public int DepartmentId { get; set; }

    public DateOnly? HiringDate { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual BranchDept BranchDept { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual Staff StaffSsnNavigation { get; set; } = null!;
}
