using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class ExamModel
{
    public int Id { get; set; }

    public int? PoolId { get; set; }

    public virtual ICollection<MultiChoiceResponse> MultiChoiceResponses { get; set; } = new List<MultiChoiceResponse>();

    public virtual Pool? Pool { get; set; }

    public virtual ICollection<StudentExamModel> StudentExamModels { get; set; } = new List<StudentExamModel>();

    public virtual ICollection<TrueFalseResponse> TrueFalseResponses { get; set; } = new List<TrueFalseResponse>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
