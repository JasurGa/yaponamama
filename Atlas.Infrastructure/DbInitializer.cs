using System;
namespace Atlas.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(AtlasDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
