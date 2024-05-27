using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DepthChart> DepthCharts { get; set; } = new List<DepthChart>();

    public virtual ICollection<TeamSport> TeamSports { get; set; } = new List<TeamSport>();
}
