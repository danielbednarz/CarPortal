using API.DTOs;
using API.Entities;
using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(Guid id);
        Task<PagedList<MessageDto>> GetUserMessages(MessageParameters messageParameters);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
    }
}
