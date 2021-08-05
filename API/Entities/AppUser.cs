using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public DateTime AccountCreationDate { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
