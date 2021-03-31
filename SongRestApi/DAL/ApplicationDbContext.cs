using Microsoft.EntityFrameworkCore;
using SongRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Song { get; set; }
    }
}
