using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Choice
{
    public int Index { get; set; }

    public int QuestionId { get; set; }

    public string? Content { get; set; }

    public bool IsCorrect { get; set; }

    public virtual ICollection<MultiChoiceResponse> MultiChoiceResponses { get; set; } = new List<MultiChoiceResponse>();

    public virtual Question Question { get; set; } = null!;
}
