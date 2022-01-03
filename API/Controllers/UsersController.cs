using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UsersController : AppController
    {
        private readonly MainDatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(MainDatabaseContext context, IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberDto>>> GetUsers([FromQuery]UserParameters userParams)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            userParams.CurrentUsername = username;

            var users = await _userRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUsers(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            var data = _mapper.Map<MemberDto>(user);

            return Ok(data);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto member)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(member, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync())
            {
                return Ok();
            }
            else
            {
                return BadRequest("Błąd przy próbie zaktualizowania użytkownika");
            }
        }

        [Authorize]
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user.Photos.Count >= 5)
            {
                return BadRequest("Możesz mieć maksymalnie 5 zdjęć");
            }

            var result = await _photoService.AddPhoto(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new Photo()
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (!await _userRepository.SaveAllAsync())
            {
                return BadRequest("Błąd przy próbie dodania zdjęcia");
            }

            var data = _mapper.Map<PhotoDto>(photo);

            return CreatedAtRoute("GetUser", new { username = user.UserName }, data);
        }

        [Authorize]
        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain)
            {
                return BadRequest("To zdjęcie jest już ustawione jako profilowe");
            }

            var currentMainPhoto = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMainPhoto != null)
            {
                currentMainPhoto.IsMain = false;
            }
            photo.IsMain = true;

            if (!await _userRepository.SaveAllAsync())
            {
                return BadRequest("Błąd przy próbie zmiany zdjęcia profilowego");
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null)
            {
                return NotFound();
            }

            if (photo.IsMain)
            {
                return BadRequest("Nie można usunąć zdjęcia profilowego");
            }

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhoto(photo.PublicId);

                if (result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }

                //user.Photos.Remove(photo);
                _context.Photos.Remove(photo);
                await _userRepository.SaveAllAsync();
            }

            return Ok();
        }
    }
}
