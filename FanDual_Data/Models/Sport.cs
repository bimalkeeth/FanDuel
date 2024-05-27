using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class Sport
{
    public int Id { get; set; }

    public string SportName { get; set; } = null!;

    public virtual ICollection<DepthChart> DepthCharts { get; set; } = new List<DepthChart>();

    public virtual ICollection<TeamSport> TeamSports { get; set; } = new List<TeamSport>();
}
