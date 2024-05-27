using System.Data;
using Castle.Core.Logging;
using FanDual_Data.Interfaces;
using FanDual_Data.Models;
using FanDual_DepthCharts;
using FanDual_DepthCharts.Business;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using DepthChart = FanDual_Data.Models.DepthChart;
using Player = FanDual_Data.Models.Player;
using Position = FanDual_Data.Models.Position;

namespace Test_Services;

public class DepthChartServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DepthChartServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test_When_AddPlayer_If_TeamId_Zero_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart()));
      
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_SportId_Zero_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart
        {
            TeamId = 12,
            
        }));
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_PositionCode_Empty_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart
        {
            TeamId = 12,
            SportId = 10,
            
        }));
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_Call_AddDepthPlayer_Data_Access_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        mockRepository.Setup(p => p.AddPlayerToChartAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>())).Throws(new DataException("error in query"));

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<DataException>(async () => await depthChartManager.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart
        {
            TeamId = 12,
            SportId = 10,
            PositionCode = "QB",
            PlayerId = 1,
            PlayerDepth = 1
        }));
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_Call_AddDepthPlayer_Data_Access_Should_Return_Success()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        mockRepository.Setup(p => p.AddPlayerToChartAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(()=>  true);

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        var result= await depthChartManager.AddPlayerToChartAsync(new RequestAddPlayerToDepthChart
        {
            TeamId = 12,
            SportId = 10,
            PositionCode = "QB",
            PlayerId = 1,
            PlayerDepth = 1
        });
        
        Assert.True(result.Success);
        
    }
    
    [Fact]
    public async Task Test_When_Remove_Player_If_TeamId_Zero_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart()));
      
    }
    
    [Fact]
    public async Task Test_When_Remove_Player_If_SportId_Zero_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart
        {
            TeamId = 1,
        }));
    }
    
    [Fact]
    public async Task Test_When_Remove_Player_If_PositionCode_Empty_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () => await depthChartManager.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart
        {
            TeamId = 1,
            SportId = 2,
        }));
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_Call_Remove_Player_Data_Access_Should_Return_Error()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        mockRepository.Setup(p => p.RemovePlayFromChartAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>()
            )).Throws(new DataException("error in query"));

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        await Assert.ThrowsAsync<DataException>(async () => await depthChartManager.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart
        {
            TeamId = 12,
            SportId = 10,
            PositionCode = "QB",
            PlayerId = 1,
        }));
    }
    
    [Fact]
    public async Task Test_When_AddPlayer_If_Call_Remove_Player_Data_Access_Should_Return_Success()
    {
        var loggerMock = new Mock<ILogger<DepthChartManager>>();
        
        loggerMock.Setup(
            x => x.Log(
                It.IsAny<LogLevel>(), 
                It.IsAny<EventId>(), 
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(), 
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)!)
        ).Callback(new InvocationAction(invocation =>
        {
            // Write out log message for testing if required.
            _testOutputHelper.WriteLine(invocation.Arguments[2].ToString());
        }));
        
        
        var mockRepository = new Mock<IRepository>();

        mockRepository.Setup(p => p.RemovePlayFromChartAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>()
        )).ReturnsAsync(()=>new SportsPlayersDepth
        {
            PositionDepth = 1,
            PlayerId = 2,
            PositionId = 3,
            DepthCharts = new DepthChart
            {
                TeamId = 3,
                Id = 3,
                SportId = 2,
                Sport = new Sport
                {
                    Id = 2,
                    SportName = "NFL",
                },
                SportsPlayersDepths =  new List<SportsPlayersDepth>
                {
                    new SportsPlayersDepth
                    {
                        PositionDepth = 1,
                        PlayerId = 1,
                        PositionId = 2,
                        Id = 78,
                        DepthChartsId = 3,
                        Position = new Position
                        {
                            Code = "QB",
                            HeadPositionId = 3,
                            Id = 2,
                            HeadPosition = new HeadPosition(),
                        },
                        Player = new Player
                        {
                            Id = 1,
                            Name = "David",
                            Number = 2,
                            
                        },
                        DepthCharts = new DepthChart
                        {
                            TeamId = 3,
                            Id = 3,
                            SportId = 2,
                            Sport = new Sport
                            {
                                Id = 2,
                                SportName = "NFL",
                            }
                        }
                    }
                },
                Team = new Team
                {
                    Id = 3,
                    Name = "Team 1",
                    
                    
                }
            },
            Player = new Player
            {
                Id = 1,
                Name = "David",
                Number = 2,
                            
            },
            Position = new Position
            {
                Code = "QB",
                HeadPositionId = 3,
                Id = 6,
                HeadPosition = new HeadPosition
                {
                    Id = 7,
                    HeadCode = "DEFENCE"
                },
            }
            
        });

        var depthChartManager = new DepthChartManager(loggerMock.Object, mockRepository.Object);

        var result= await depthChartManager.RemovePlayerFromChartAsync(new RequestRemovePlayerFromDepthChart
        {
            TeamId = 12,
            SportId = 10,
            PositionCode = "QB",
            PlayerId = 2,
        });
        
        Assert.Equal(2,result.PlayerDepth.PlayerId);
    }
}

