using Plarium.Domain.Entities;
using System.Data.Entity;

namespace Plarium.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(): base("DbConnection")
        {
            
        }
         
        public DbSet<LogMessage> LogMessage { get; set; }
    }
}
