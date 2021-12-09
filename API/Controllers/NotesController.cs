using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class NotesController : AppController
    {
        private readonly MainDatabaseContext _context;
        private readonly INotesRepository _notesRepository;
        private readonly IUserRepository _userRepository;

        public NotesController(MainDatabaseContext context, INotesRepository notesRepository, IUserRepository userRepository)
        {
            _context = context;
            _notesRepository = notesRepository;
            _userRepository = userRepository;
        }

        [HttpGet("getNotes/{userId}")]
        public async Task<ActionResult<List<Note>>> GetNotes(int userId)
        {
            var data = await _notesRepository.GetNotesToList(userId);

            return Ok(data);
        }

        [HttpPost("add-note")]
        public async Task<ActionResult<Note>> AddNote(Note note)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            note.UserId = user.Id;

            _context.Notes.Add(note);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("delete-note/{noteId}")]
        public async Task<ActionResult> DeleteNote(string noteId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            var note = _context.Notes.FirstOrDefault(x => x.Id == Guid.Parse(noteId));

            if(note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _userRepository.SaveAllAsync();

            return Ok();
        }

    }
}
