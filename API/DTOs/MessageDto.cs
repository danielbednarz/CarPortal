using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public int SenderBrandId { get; set; }
        public int SenderModelId { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public int RecipientBrandId { get; set; }
        public int RecipientModelId { get; set; }
        public string Content { get; set; }
        public DateTime? MessageReadDate { get; set; }
        public DateTime MessageSentDate { get; set; }
        public bool IsSenderDeleted { get; set; }
        public bool IsRecipientDeleted { get; set; }
    }
}
