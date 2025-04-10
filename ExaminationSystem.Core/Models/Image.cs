using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Image
{
    public string Id { get; set; } = null!;

    public int? QuestionId { get; set; }

    public string Url { get; set; } = null!;

    public virtual Question? Question { get; set; }
}
