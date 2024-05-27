using FanDual_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FanDual_Data.Seeds;



public class SeedService
{
    private readonly FanDualContext _fanDualContext;

    public SeedService(FanDualContext fanDualContext)
    {
        _fanDualContext = fanDualContext;

        SeedData();
    }
   
    private void SeedData()
    {
        var headPositionList = new List<HeadPosition>
        {
            new () { HeadCode = "Default" },
            new () { HeadCode = "OFFENSE", },
            new () { HeadCode = "DEFENSE" },
            new () { HeadCode = "SPECIAL TEAMS" }
        };
        
        _fanDualContext.HeadPositions.AddRange(headPositionList);
        
        var offence = headPositionList.First(s => s.HeadCode == "OFFENSE");
        var defence = headPositionList.First(s => s.HeadCode == "DEFENSE");
        var special = headPositionList.First(s => s.HeadCode == "SPECIAL TEAMS");

        var positionList = new List<Position>
        {
            new () { HeadPosition = offence, Code = "LWR" },
            new () { HeadPosition = offence, Code = "RWR" },
            new () { HeadPosition = offence, Code = "SWR" },
            new () { HeadPosition = offence, Code = "LT" },
            new () { HeadPosition = offence, Code = "LG" },
            new () { HeadPosition = offence, Code = "C" },
            new () { HeadPosition = offence, Code = "RG" },
            new () { HeadPosition = offence, Code = "RT" },
            new () { HeadPosition = offence, Code = "TE" },
            new () { HeadPosition = offence, Code = "QB" },
            new () { HeadPosition = offence, Code = "RB" },

            new () { HeadPosition = defence, Code = "LDE" },
            new () { HeadPosition = defence, Code = "NT" },
            new () { HeadPosition = defence, Code = "RDE" },
            new () { HeadPosition = defence, Code = "LOLB" },
            new () { HeadPosition = defence, Code = "LILB" },
            new () { HeadPosition = defence, Code = "RILB" },
            new () { HeadPosition = defence, Code = "ROLB" },
            new () { HeadPosition = defence, Code = "LCB" },
            new () { HeadPosition = defence, Code = "SS" },
            new () { HeadPosition = defence, Code = "FS" },
            new () { HeadPosition = defence, Code = "RCB" },
            new () { HeadPosition = defence, Code = "NB" },

            new () { HeadPosition = special, Code = "PT" },
            new () { HeadPosition = special, Code = "PK" },
            new () { HeadPosition = special, Code = "LS" },
            new () { HeadPosition = special, Code = "H" },
            new () { HeadPosition = special, Code = "KD" },
            new () { HeadPosition = special, Code = "PR" },
            new () { HeadPosition = special, Code = "KR" },
        };
        _fanDualContext.Positions.AddRange(positionList);


        var sportList = new List<Sport>
        {
            new() { SportName = "NFL" },
            new() { SportName = "MLB" },
            new() { SportName = "NHL" },
            new() { SportName = "NBA" }
        };
        _fanDualContext.Sports.AddRange(sportList);
        
        
       _fanDualContext.Teams.AddRange(new List<Team>
        {
            new Team{Name = "Tampa Bay Buccaneers"}
        });

        _fanDualContext.SaveChanges();

        var team = _fanDualContext.Teams.First(s => s.Name == "Tampa Bay Buccaneers");
        var sport = _fanDualContext.Sports.First(s => s.SportName == "NFL");

        _fanDualContext.TeamSports.AddRange(new List<TeamSport>
        {
            new TeamSport
            {
                Team = team,
                Sport = sport,
            }
        });

        var playerList = new List<Player>
        {
            new() { Name = "EVANS, MIKE 14/1", Number = 13 },
            new() { Name = "Jarrett, Rakim CF23", Number = 18 },
            new() { Name = "Johnson III, Cephus SF23", Number = 28 },
            new() { Name = "Knue, Tanner CF24", Number = 80 },
            new() { Name = "Palmer, Trey 23/6", Number = 10 },
            new() { Name = "Palmer, Trey 23/6", Number = 15 },
            new() { Name = "Miller, Ryan CF23", Number = 81 },
            new() { Name = "Miller, Ryan CF23", Number = 85 },
            new() { Name = "Godwin, Chris 17/3", Number = 14 },
            new() { Name = "Thompkins, Deven CF22", Number = 83 },
            new() { Name = "Webb, Raleigh SF23", Number = 17 },
            new() { Name = "Johnson, Kameron CF24", Number = 9 },
            new() { Name = "Wirfs, Tristan 20/1", Number = 78 },
            new() { Name = "Skule, Justin SF22", Number = 77 },
            new() { Name = "Dzansi, Silas CF23", Number = 61 },
            new() { Name = "Bredeson, Ben U/NYG", Number = 68 },
            new() { Name = "Klein, Elijah 24/6", Number = 79 },
            new() { Name = "Delgado, Xavier CF24", Number = 60 },
            new() { Name = "Barton, Graham 24/1", Number = 62 },
            new() { Name = "Hainsey, Robert 21/3", Number = 70 },
            new() { Name = "Jones, Avery CF24", Number = 66 },
            new() { Name = "Mauch, Cody 23/2", Number = 69 },
            new() { Name = "Opeta, Sua U/Phi", Number = 76 },
            new() { Name = "Haggard, Luke CF23", Number = 72 },
            new() { Name = "Metz, Lorenz IPP", Number = 71 },
            new() { Name = "Mayfield, Baker U/LAR", Number = 6 },
            new() { Name = "Trask, Kyle 21/2", Number = 2 },
            new() { Name = "Wolford, John U/LAR", Number = 11 },
            new() { Name = "Annexstad, Zack CF24", Number = 19 },
            new() { Name = "White, Rachaad 22/3", Number = 1 },
            new() { Name = "Irving, Bucky 24/4", Number = 7 },
            new() { Name = "Edmonds, Chase CC/Den", Number = 22 },
            new() { Name = "Tucker, Sean CF23", Number = 44 },
            new() { Name = "Williams, D.J. CF24", Number = 30 },
            new() { Name = "Jefferson, Ramon CF24", Number = 90 },
            new() { Name = "Hall, Logan 22/2", Number = 13 },
            new() { Name = "GHOLSTON, WILLIAM 13/4", Number = 92 },
            new() { Name = "Brewer, C.J. SF23", Number = 95 },
            new() { Name = "Uguak, Lwal SF24", Number = 75 },
            new() { Name = "Brown IV, Earnest SF24", Number = 100 },
            new() { Name = "Vea, Vita 18/1", Number = 50 },
            new() { Name = "Gaines, Greg U/LAR", Number = 96 },

            new() { Name = "Kancey, Calijah 23/1", Number = 94 },
            new() { Name = "Greene, Mike CF22", Number = 91 },
            new() { Name = "Banks, Eric SF24", Number = 93 },
            new() { Name = "Culpepper, Judge CF24", Number = 59 },
            new() { Name = "Tryon-Shoyinka, Joe 21/1", Number = 9 },
            new() { Name = "Nelson, Anthony 19/4", Number = 33 },
            new() { Name = "Ramirez, Jose 23/6", Number = 96 },
            new() { Name = "Watts, Markees CF23", Number = 58 },
            new() { Name = "Grzesiak, Daniel CF24", Number = 57 },
            new() { Name = "Britt, K.J. 21/5", Number = 52 },
            new() { Name = "Russell, J.J. CF22", Number = 51 },
            new() { Name = "Jones, Vi SF23", Number = 53 },
            new() { Name = "Grier Jr., Antonio CF24", Number = 48 },
            new() { Name = "DAVID, LAVONTE 12/2", Number = 54 },
            new() { Name = "Dennis, SirVocea 23/5", Number = 8 },
            new() { Name = "DeLoach, Kalen CF24", Number = 46 },

            new() { Name = "Diaby, Yaya 23/3", Number = 0 },
            new() { Name = "Braswell, Chris 24/2", Number = 43 },
            new() { Name = "GREGORY, RANDY U/SF", Number = 56 },
            new() { Name = "Peterson Jr., Shaun CF24", Number = 46 },
            new() { Name = "McCollum, Zyon 22/5", Number = 27 },
            new() { Name = "Hall, Bryce U/NYJ", Number = 34 },
            new() { Name = "Funderburk, Tyrek CF24", Number = 24 },
            new() { Name = "Hayes, Andrew CF24", Number = 21 },
            new() { Name = "Whitehead, Jordan 18/4", Number = 3 },
            new() { Name = "Merriweather, Kaevon CF23", Number = 26 },
            new() { Name = "Wisdom, Rashad CF24", Number = 38 },
            new() { Name = "Winfield Jr., Antoine 20/2", Number = 31 },
            new() { Name = "Izien, Christian CF23", Number = 29 },

            new() { Name = "Banks, Marcus CF24", Number = 39 },
            new() { Name = "Dean, Jamel 19/3", Number = 35 },
            new() { Name = "Hayes, Josh 23/6", Number = 32 },
            new() { Name = "Isaac, Keenan CF23", Number = 16 },
            new() { Name = "Smith, Tykee 24/3", Number = 23 },
            new() { Name = "Thomas, Tavierre U/Hou", Number = 37 },
            new() { Name = "McDonald, Chris CF24", Number = 36 },
            new() { Name = "Camarda, Jake 22/4", Number = 5 },

            new() { Name = "McLaughlin, Chase U/Ind", Number = 4 },
            new() { Name = "TRINER, ZACH SF19", Number = 97 },
            new() { Name = "Thompkins, Deven CF22", Number = 83 },
            new() { Name = "Deckers, Evan CF23", Number = 86 },
            new() { Name = "Palmer, Trey 23/6", Number = 10 },
            new() { Name = "White, Rachaad 22/3", Number = 1 },
        };
        
        _fanDualContext.Players.AddRange(playerList);


        var qb = positionList.First(w => w.Code == "QB");
        var player0 = playerList.First(p => p.Name == "Mayfield, Baker U/LAR");
        var player1 = playerList.First(p => p.Name == "Trask, Kyle 21/2");
        var player2 = playerList.First(p => p.Name == "Wolford, John U/LAR");
        var player3 = playerList.First(p => p.Name == "Annexstad, Zack CF24");
        
        
        var lwr = positionList.First(w => w.Code == "LWR");
        var playerlwr0 = playerList.First(p => p.Name == "EVANS, MIKE 14/1");
        var playerlwr1 = playerList.First(p => p.Name == "Jarrett, Rakim CF23");
        var playerlwr2 = playerList.First(p => p.Name == "Johnson III, Cephus SF23");
        var playerlwr3 = playerList.First(p => p.Name == "Knue, Tanner CF24");

        var depthChart = new DepthChart { Sport = sport, Team = team };
        
        var sportDepthList = new List<SportsPlayersDepth>
        {
            new() { Position = qb, Player = player0, PositionDepth = 0,DepthCharts = depthChart},
            new() { Position = qb, Player = player1, PositionDepth = 1,DepthCharts = depthChart },
            new() { Position = qb, Player = player2, PositionDepth = 2 ,DepthCharts = depthChart},
            new() { Position = qb, Player = player3, PositionDepth = 3 ,DepthCharts = depthChart},
            
            new() { Position = lwr, Player = playerlwr0, PositionDepth = 0 ,DepthCharts = depthChart},
            new() { Position = lwr, Player = playerlwr1, PositionDepth = 1 ,DepthCharts = depthChart},
            new() { Position = lwr, Player = playerlwr2, PositionDepth = 2 ,DepthCharts = depthChart},
            new() { Position = lwr, Player = playerlwr3, PositionDepth = 3 ,DepthCharts = depthChart},
        };
       
        _fanDualContext.DepthCharts.Add(depthChart);
        
        _fanDualContext.SportsPlayersDepths.AddRange(sportDepthList);

        _fanDualContext.SaveChanges();
    }
}