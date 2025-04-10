using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class TrueFalseResponse
{
    public long StdSsn { get; set; }

    public int ExamId { get; set; }

    public int QuestionId { get; set; }

    public bool Answer { get; set; }

    public virtual ExamModel Exam { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Student StdSsnNavigation { get; set; } = null!;
}
