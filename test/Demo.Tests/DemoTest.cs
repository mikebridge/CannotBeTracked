using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Demo.Tests
{
    public class DemoTest : IDisposable
    {
        private DemoDbContext _ctx;

        public DemoTest()
        {
            _ctx = DbContextFixture.InMemoryContext();
        }
        public void Dispose()
        {            
            _ctx.Dispose();
        }

        [Fact]
        public async Task It_Should_Update_A_Message()
        {

            // The instance of entity type 'FacultyTag' cannot be tracked because another instance of this type with the same key is already being tracked.
            // possibly https://github.com/aspnet/EntityFrameworkCore/issues/7084
            // Arrange
            var service = new MessageService(_ctx);
            var messageId = Guid.NewGuid();
            var oldRecord = new AddOrUpdateMessageViewModel
            {
                Id = messageId,
                Body = "test",
                Tags = new[] {"test1", "test2"}
            };
            await service.AddOrUpdateAsync(oldRecord);
            await _ctx.SaveChangesAsync();

            // Act
            var newRecord = new AddOrUpdateMessageViewModel
            {
                Id = messageId,
                Body = "test updated",
                Tags = new[] { "test1", "test2" }
            };
            await service.AddOrUpdateAsync(newRecord);
            await _ctx.SaveChangesAsync();
            var foundRecord = await _ctx.Message
                                        .Include(x => x.Tags)
                                        .FirstOrDefaultAsync(m => m.Id == messageId);

            // Assert
            Assert.NotNull(foundRecord);
            Assert.Equal(newRecord.Body, foundRecord.Body);
            Assert.Equal(newRecord.Tags, foundRecord.Tags.Select(t => t.Tag));

            Assert.NotNull(foundRecord.Tags);

        }



    }
}
