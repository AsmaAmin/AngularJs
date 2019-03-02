using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence_11CoreAPI_Angularjs.Models
{
    public class DbGames : IdentityDbContext
    {
        public DbGames(DbContextOptions<DbGames> options) : base(options)
        {

        }
        public DbSet <Game> Games { get; set; } 
    }

    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public int MyProperty { get; set; }
    }
}
