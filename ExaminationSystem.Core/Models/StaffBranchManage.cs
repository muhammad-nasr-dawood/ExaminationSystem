using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class StaffBranchManage
{
    public long StaffSsn { get; set; }

    public int BranchId { get; set; }

    public DateOnly? HiringDate { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Staff StaffSsnNavigation { get; set; } = null!;
}
