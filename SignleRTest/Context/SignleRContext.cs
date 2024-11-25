using Microsoft.EntityFrameworkCore;
using SignleRTest.Models;

namespace SignleRTest.Context
{
    public class SignleRContext :DbContext
    {
        public SignleRContext(DbContextOptions<SignleRContext> context):base(context)
        {
            
        }
       public DbSet<Message> messages { get; set; }
    }
}
