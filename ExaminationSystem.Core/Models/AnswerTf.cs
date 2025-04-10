using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class AnswerTf
{
    public int QuestionId { get; set; }

    public bool IsTrue { get; set; }

    public virtual Question Question { get; set; } = null!;
}
