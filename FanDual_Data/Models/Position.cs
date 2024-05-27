using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class Position
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public int HeadPositionId { get; set; }

    public virtual HeadPosition HeadPosition { get; set; } = null!;

    public virtual ICollection<SportsPlayersDepth> SportsPlayersDepths { get; set; } = new List<SportsPlayersDepth>();
}
