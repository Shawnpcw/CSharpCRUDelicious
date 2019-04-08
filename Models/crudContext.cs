using Microsoft.EntityFrameworkCore;
 
namespace CRUDelicious.Models
{
    public class crudContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public crudContext(DbContextOptions<crudContext> options) : base(options) { }
            public DbSet<Dish> dishes {get;set;}
        
    }
}
