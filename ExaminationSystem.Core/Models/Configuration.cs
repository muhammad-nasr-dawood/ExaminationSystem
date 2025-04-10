using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Configuration
{
    public int PoolId { get; set; }

    public byte NoOfDifficult { get; set; }

    public byte NoOfMedium { get; set; }

    public byte NoOfEasy { get; set; }

    public byte GradeForDifficult { get; set; }

    public byte GradeForMedium { get; set; }

    public byte GradeForEasy { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? StartingTime { get; set; }

    public TimeOnly? EndingTime { get; set; }

    public int? Duration { get; set; }

    public byte NoOfModels { get; set; }

    public bool CanModify { get; set; }

    public virtual Pool Pool { get; set; } = null!;
}
