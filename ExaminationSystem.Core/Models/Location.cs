using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Location
{
    public string ZipCode { get; set; } = null!;

    public string Governate { get; set; } = null!;

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
