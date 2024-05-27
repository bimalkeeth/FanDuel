using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class HeadPosition
{
    public int Id { get; set; }

    public string HeadCode { get; set; } = null!;

    public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
}
