﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

[Keyless]
public partial class Session
{
    public int? Duration { get; set; }

    public TimeOnly? StartingTime { get; set; }

    public TimeOnly? EndingTime { get; set; }
}