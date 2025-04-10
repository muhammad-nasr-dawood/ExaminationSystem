using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class MultiChoiceResponse
{
    public long StdSsn { get; set; }

    public int ExamId { get; set; }

    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public virtual Choice Choice { get; set; } = null!;

    public virtual ExamModel Exam { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;

    public virtual Student StdSsnNavigation { get; set; } = null!;
}
