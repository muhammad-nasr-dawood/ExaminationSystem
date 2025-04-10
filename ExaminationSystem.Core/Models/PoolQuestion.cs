using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class PoolQuestion
{
    public int PoolId { get; set; }

    public int QuestionId { get; set; }

    public byte? IsIncluded { get; set; }

    public virtual Pool Pool { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
