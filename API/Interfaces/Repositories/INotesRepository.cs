using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface INotesRepository
    {
        Task<List<Note>> GetNotesToList(int userId);
    }
}
