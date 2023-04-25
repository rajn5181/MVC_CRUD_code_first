using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcCrud.Models.Domain;
using System.Data;

namespace MvcCrud.Data
{
    public class MydemoContext:DbContext
    {
        public MydemoContext(DbContextOptions options) : base(options) { }

        public static object Employee { get; internal set; }
        public DbSet<Employee> employe { get; set; }
     
    }
}
