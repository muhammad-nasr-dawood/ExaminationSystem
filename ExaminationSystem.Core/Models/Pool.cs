using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Pool
{
    public int Id { get; set; }

    public byte IsActive { get; set; }

    public long StaffId { get; set; }

    public int DeptId { get; set; }

    public int BranchId { get; set; }

    public int CourseId { get; set; }

    public string? Title { get; set; }

    public int IntakeId { get; set; }

    public int NoOfMedium { get; set; }

    public int NoOfEasy { get; set; }

    public int NoOfDifficult { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Configuration? Configuration { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Department Dept { get; set; } = null!;

    public virtual ICollection<ExamModel> ExamModels { get; set; } = new List<ExamModel>();

    public virtual Intake Intake { get; set; } = null!;

    public virtual ICollection<PoolQuestion> PoolQuestions { get; set; } = new List<PoolQuestion>();

    public virtual Staff Staff { get; set; } = null!;
}
