using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class StudentExamModel
{
    public long StudentId { get; set; }

    public int ExamModelId { get; set; }

    public bool? Attendance { get; set; }

    public int? Total { get; set; }

    public virtual ExamModel ExamModel { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
