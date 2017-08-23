using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Demo.Tests
{
    public class DbContextFixture
    {
        public static DemoDbContext InMemoryContext()
        {
            // SEE: https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite
            var connection = new SqliteConnection("Data Source=:memory:");
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseSqlite(connection)
                .Options;
            connection.Open();
            // create the schema
            using (var context = new DemoDbContext(options))
            {
                context.Database.EnsureCreated();
            }

            return new DemoDbContext(options);

        }
    }
}
