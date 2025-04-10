using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Session
{
    public int? Duration { get; set; }

    public TimeOnly? StartingTime { get; set; }

    public TimeOnly? EndingTime { get; set; }
}
