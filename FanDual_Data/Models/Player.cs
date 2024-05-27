using System;
using System.Collections.Generic;

namespace FanDual_Data.Models;

public partial class Player
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Number { get; set; }

    public virtual ICollection<SportsPlayersDepth> SportsPlayersDepths { get; set; } = new List<SportsPlayersDepth>();
}
