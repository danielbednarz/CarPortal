using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly MainDatabaseContext _context;

        public NotesRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetNotesToList(int userId)
        {
            return await _context.Notes.Where(u => u.UserId == userId).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }
    }
}
