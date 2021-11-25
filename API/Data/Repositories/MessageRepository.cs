using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);

            _context.SaveChanges();
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(Guid id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Include(x => x.Sender).ThenInclude(y => y.Brand)
                .Include(x => x.Sender).ThenInclude(y => y.Model)
                .Include(x => x.Recipient).ThenInclude(y => y.Brand)
                .Include(x => x.Recipient).ThenInclude(y => y.Model)
                .Where(x => x.Recipient.UserName == recipientUsername && x.Sender.UserName == currentUsername || x.Recipient.UserName == currentUsername && x.Sender.UserName == recipientUsername)
                .OrderByDescending(m => m.MessageSentDate)
                .ToListAsync();

            var unreadMessages = messages.Where(x => x.MessageReadDate == null && x.Recipient.UserName == currentUsername).ToList();

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.MessageReadDate = DateTime.Now;
                }
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<PagedList<MessageDto>> GetUserMessages(MessageParameters messageParameters)
        {
            var query = _context.Messages.OrderByDescending(x => x.MessageSentDate).AsQueryable();

            query = messageParameters.Container switch
            {
                "Inbox" => query.Where(x => x.RecipientUsername == messageParameters.Username),
                "Outbox" => query.Where(x => x.SenderUsername == messageParameters.Username),
                _ => query.Where(x => x.RecipientUsername == messageParameters.Username && x.MessageReadDate == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<MessageDto>.CreateAsync(messages, messageParameters.PageNumber, messageParameters.PageSize);
        }
    }
}
