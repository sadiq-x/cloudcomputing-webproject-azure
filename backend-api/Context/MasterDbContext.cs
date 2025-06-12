using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend_api.Context
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Person> Person { get; set; }
    }

    [Table("Cars")]
    public class Cars
    {
        [Key]
        public int? Id { get; set; }
        public string? Mark { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? Km { get; set; }
        public string? Price { get; set; } 
        public string? Year { get; set; } 
    }

    [Table("Person")]
    public class Person
    {
        [Key]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? JobPosition { get; set; }
        public string? Gender { get; set; }
        public string? Age { get; set; }
        public string? Location { get; set; } 
        public string? Hobby { get; set; } 
    }
}