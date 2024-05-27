using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class DepthChart
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int SportId { get; set; }

    public virtual Sport Sport { get; set; } = null!;

    public virtual ICollection<SportsPlayersDepth> SportsPlayersDepths { get; set; } = new List<SportsPlayersDepth>();

    public virtual Team Team { get; set; } = null!;
}
