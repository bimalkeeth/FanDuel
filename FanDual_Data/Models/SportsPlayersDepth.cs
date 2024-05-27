using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class SportsPlayersDepth
{
    public int Id { get; set; }

    public int PositionId { get; set; }

    public int PlayerId { get; set; }

    public int PositionDepth { get; set; }

    public int DepthChartsId { get; set; }

    public virtual DepthChart DepthCharts { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
