using System;
using System.Collections.Generic;

namespace ExaminationSystem.Core.Models;

public partial class Question
{
    public int Id { get; set; }

    public byte Type { get; set; }

    public byte Level { get; set; }

    public int TopicId { get; set; }

    public long? StaffId { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public byte IsDeleted { get; set; }

    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<MultiChoiceResponse> MultiChoiceResponses { get; set; } = new List<MultiChoiceResponse>();

    public virtual ICollection<PoolQuestion> PoolQuestions { get; set; } = new List<PoolQuestion>();

    public virtual Staff? Staff { get; set; }

    public virtual Topic Topic { get; set; } = null!;

    public virtual ICollection<TrueFalseResponse> TrueFalseResponses { get; set; } = new List<TrueFalseResponse>();

    public virtual ICollection<ExamModel> ExamModels { get; set; } = new List<ExamModel>();
}
