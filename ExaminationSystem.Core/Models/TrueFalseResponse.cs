﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

[PrimaryKey("StdSsn", "ExamId", "QuestionId")]
[Table("TrueFalseResponse")]
public partial class TrueFalseResponse
{
    [Key]
    [Column("STD_SSN")]
    public long StdSsn { get; set; }

    [Key]
    [Column("Exam_ID")]
    public int ExamId { get; set; }

    [Key]
    [Column("Question_ID")]
    public int QuestionId { get; set; }

    public bool Answer { get; set; }

    [ForeignKey("ExamId")]
    [InverseProperty("TrueFalseResponses")]
    public virtual ExamModel Exam { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("TrueFalseResponses")]
    public virtual Question Question { get; set; }

    [ForeignKey("StdSsn")]
    [InverseProperty("TrueFalseResponses")]
    public virtual Student StdSsnNavigation { get; set; }
}