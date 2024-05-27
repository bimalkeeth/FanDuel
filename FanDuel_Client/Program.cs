// See https://aka.ms/new-console-template for more information

using FanDual_Data.Models;
using FanDual_Data.Seeds;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var dbcontext =new FanDualContext(new DbContextOptions<FanDualContext>{});

var seedService =new SeedService(dbcontext);