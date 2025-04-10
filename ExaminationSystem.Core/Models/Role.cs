using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Staff> StaffSsns { get; set; } = new List<Staff>();
}
