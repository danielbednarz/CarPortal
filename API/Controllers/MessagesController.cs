using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class MessagesController : AppController
    {
        private readonly MainDatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessagesController(MainDatabaseContext context, IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _context = context;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (username == createMessageDto.RecipientUsername.ToLower())
            {
                return BadRequest("Nie możesz wysłać wiadomości do siebie");
            }

            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

            if(recipient == null)
            {
                return NotFound();
            }

            var message = new Message
            {
                Sender = sender,
                SenderUsername = sender.UserName,
                Recipient = recipient,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };

            _messageRepository.AddMessage(message);

            return Ok(_mapper.Map<MessageDto>(message));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetUserMessages([FromQuery] MessageParameters messageParameters)
        {
            messageParameters.Username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var messages = await _messageRepository.GetUserMessages(messageParameters);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var data = await _messageRepository.GetMessageThread(currentUsername, username);

            return Ok(data);
        }

        [HttpDelete("{messageId}")]
        public async Task<ActionResult> DeleteMessage(string messageId)
        {
            var currentUsername = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var message = await _messageRepository.GetMessage(Guid.Parse(messageId));

            if(message.Sender.UserName != currentUsername && message.Recipient.UserName != currentUsername)
            {
                return Unauthorized();
            }

            if(message.Sender.UserName == currentUsername)
            {
                message.IsSenderDeleted = true;
            }

            if(message.Recipient.UserName == currentUsername)
            {
                message.IsRecipientDeleted = true;
            }

            if(message.IsSenderDeleted && message.IsRecipientDeleted)
            {
                _messageRepository.DeleteMessage(message);
            }

            if(await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Błąd przy próbie usunięcia zdjęcia");
            }

        }
    }
}
