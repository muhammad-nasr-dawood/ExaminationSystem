﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

public partial class Image
{
    [Key]
    [StringLength(25)]
    [Unicode(false)]
    public string Id { get; set; }

    public int? QuestionId { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Url { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("Images")]
    public virtual Question Question { get; set; }
}