using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class MessageService
    {
        private readonly DemoDbContext _ctx;

        public MessageService(DemoDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddOrUpdateAsync(AddOrUpdateMessageViewModel messageViewModel)
        {
            Message existingMessage = null;

            if (messageViewModel.Id.HasValue)
            {
                existingMessage = await _ctx.Message.FindAsync(messageViewModel.Id.Value);
            }

            if (existingMessage == null)
            {
                var newMessage = new Message
                {
                    Id = messageViewModel.Id ?? Guid.NewGuid(),
                    Body = messageViewModel.Body,
                    Tags = AsCollection(messageViewModel.Tags),
                    DateCreated = DateTime.UtcNow

                };
                await _ctx.Message.AddAsync(newMessage);
            }
            else
            {
                existingMessage.Body = messageViewModel.Body;
                existingMessage.Tags = AsCollection(existingMessage.Tags, messageViewModel.Tags);               
            }
            await _ctx.SaveChangesAsync();
        }

        private static Collection<MessageTag> AsCollection(string[] tags)
        {
            return new Collection<MessageTag>(tags.Select(tag => new MessageTag { DateCreated = DateTime.UtcNow, Tag = tag }).ToList());
        }

        private static Collection<MessageTag> AsCollection(ICollection<MessageTag> existingMessageTags, string[] tags)
        {
            var tagsToKeep = existingMessageTags.Where(tag => tags.Contains(tag.Tag)).ToList();
            var newTags = tags.Except(tagsToKeep.Select(x => x.Tag))
                .Select(tag => new MessageTag {DateCreated = DateTime.UtcNow, Tag = tag});

            return new Collection<MessageTag>(tagsToKeep.Concat(newTags).ToList());

        }
    }
}
